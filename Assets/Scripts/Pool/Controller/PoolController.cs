using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FYG.Pooling
{
    public class PoolController : MonoBehaviour
    {
        [SerializeField] private List<PoolConfig> m_Pools = new List<PoolConfig>();
        private Dictionary<string, Pool> CachedPools = new Dictionary<string, Pool>();
        private readonly List<AsyncOperationHandle<GameObject>> m_LoadHandles = new List<AsyncOperationHandle<GameObject>>();

        private void Awake()
        {
            InitializePools();
        }

        private void OnEnable()
        {
            Events.OnSpawn += Spawn;
            Events.OnDespawn += Despawn;
        }
        
        private void OnDisable()
        {
            Events.OnSpawn -= Spawn;
            Events.OnDespawn -= Despawn;
        }

        private void OnDestroy()
        {
            // Release the loaded Addressable prefabs
            for (int i = 0; i < m_LoadHandles.Count; i++)
            {
                if (m_LoadHandles[i].IsValid())
                    Addressables.Release(m_LoadHandles[i]);
            }
            m_LoadHandles.Clear();
        }

        private void InitializePools()
        {
            for (int i = 0; i < m_Pools.Count; i++)
            {
                var config = m_Pools[i];

                if (string.IsNullOrEmpty(config.Tag) || config.Prefab == null || !config.Prefab.RuntimeKeyIsValid())
                    continue;

                if (CachedPools.ContainsKey(config.Tag))
                {
                    Debug.LogWarning($"Duplicate pool tag {config.Tag}, skipping.");
                    continue;
                }

                Pool pool = new Pool();
                CachedPools.Add(config.Tag, pool);

                var handle = Addressables.LoadAssetAsync<GameObject>(config.Prefab);
                m_LoadHandles.Add(handle);

                int initialSize = config.InitialSize;
                string tag = config.Tag;
                handle.Completed += op =>
                {
                    if (op.Status != AsyncOperationStatus.Succeeded)
                    {
                        Debug.LogError($"Failed to load Addressable prefab for pool {tag}.");
                        return;
                    }

                    pool.Prefab = op.Result;
                    for (int j = 0; j < initialSize; j++)
                    {
                        CreateNewObject(pool);
                    }
                };
            }
        }

        private PooledObject CreateNewObject(Pool pool)
        {
            GameObject go = Instantiate(pool.Prefab);
            go.SetActive(false);

            PooledObject po = new PooledObject(go);
            pool.Objects.Add(po);

            return po;
        }

        // ==========================
        // SPAWN
        // ==========================
        public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
        {
            if (!CachedPools.TryGetValue(tag, out Pool pool))
            {
                Debug.LogError($"Pool with tag {tag} not found.");
                return null;
            }

            if (pool.Prefab == null)
            {
                Debug.LogWarning($"Pool {tag} is still loading its Addressable prefab.");
                return null;
            }

            PooledObject selected = null;

            int count = pool.Objects.Count;

            // 1️⃣ Try next index
            if (count > 0)
            {
                var candidate = pool.Objects[pool.NextIndex];
                if (!candidate.IsTaken)
                {
                    selected = candidate;
                }
            }

            // 2️⃣ Find oldest available
            if (selected == null)
            {
                float oldestTime = float.MaxValue;

                for (int i = 0; i < count; i++)
                {
                    var obj = pool.Objects[i];
                    if (!obj.IsTaken && obj.LastUsedTime < oldestTime)
                    {
                        oldestTime = obj.LastUsedTime;
                        selected = obj;
                    }
                }
            }

            // 3️⃣ If none free → create new
            if (selected == null)
            {
                selected = CreateNewObject(pool);
            }

            // Activate
            selected.IsTaken = true;
            selected.LastUsedTime = Time.time;

            GameObject go = selected.GameObject;
            go.transform.SetPositionAndRotation(position, rotation);
            go.SetActive(true);

            // Advance pointer
            pool.NextIndex++;
            if (pool.NextIndex >= pool.Objects.Count)
                pool.NextIndex = 0;

            return go;
        }

        // ==========================
        // DESPAWN
        // ==========================
        public void Despawn(string tag, GameObject obj)
        {
            if (!CachedPools.TryGetValue(tag, out Pool pool))
            {
                Debug.LogWarning($"Pool with tag {tag} not found.");
                return;
            }

            var list = pool.Objects;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GameObject == obj)
                {
                    list[i].IsTaken = false;
                    list[i].LastUsedTime = Time.time;
                    obj.SetActive(false);
                    return;
                }
            }

            Debug.LogWarning("Tried to despawn object not belonging to pool.");
        }
    }
}