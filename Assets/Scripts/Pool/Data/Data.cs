using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace FYG.Pooling
{
    [System.Serializable]
    public class PoolConfig
    {
        public string Tag;
        public AssetReferenceGameObject Prefab;
        public int InitialSize = 5;
    }

    public class PooledObject
    {
        public GameObject GameObject;
        public bool IsTaken;
        public float LastUsedTime;

        public PooledObject(GameObject go)
        {
            GameObject = go;
            IsTaken = false;
            LastUsedTime = -1f;
        }
    }

    public class Pool
    {
        public GameObject Prefab;
        public List<PooledObject> Objects = new List<PooledObject>();
        public int NextIndex = 0;
    }
}