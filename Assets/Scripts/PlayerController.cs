using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using PoolEvents = FYG.Pooling.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool IsOn;
    private Animator m_animator;
    private AudioSource audioSource;
    [SerializeField] AudioSource FootStepSound;

    [SerializeField] Light TorchLight;
    [SerializeField] AudioClip TorchOnSound;
    private float Vertical, Horizontal;
    [SerializeField] float m_turnSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] KeyCode m_enterCarKey;
    [SerializeField] List<String> PoolObjectKey = new List<string>();

    void Start()
    {
        m_animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            FootStepSound.enabled = true;
            // FootStepSound.pitch = UnityEngine.Random.Range(0.08f, 1f);
        }
        else
        {
            FootStepSound.enabled = false;
        }
        PlayerTorch();
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        m_animator.SetFloat("Vertical", Vertical, 0.1f, Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime * m_turnSpeed * Horizontal);
    }

    private void PlayerTorch()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IsOn = !IsOn;
            audioSource.PlayOneShot(TorchOnSound);
        }
        if (!IsOn)
        {
            TorchLight.enabled = false;
        }
        if (IsOn)
        {
            TorchLight.enabled = true;
        }
    }

    public void OnTriggerStay(Collider Other)
    {
        // Retry while standing inside a spawn point (e.g. the pool was still loading on enter)
        if (Other.TryGetComponent(out SpawnPoints spawnPoint))
            TrySpawn(spawnPoint, false);

        if (Other.gameObject.tag.Equals("Public"))
        {
            int CarId = Other.gameObject.GetComponent<CarIdentity>().m_carId;
            if (Input.GetKeyDown(m_enterCarKey))
            {
                Events.DoPersonDeActivate();
                Events.DoCarActivate(CarId);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Only spawn points matter here
        if (other.TryGetComponent(out SpawnPoints spawnPoint))
            TrySpawn(spawnPoint, true);
    }

    private void TrySpawn(SpawnPoints spawnPoint, bool logReason)
    {
        // Already spawned for this point
        if (spawnPoint.SpawnedObject != null && spawnPoint.SpawnedObject.activeSelf)
            return;

        string key = spawnPoint.GetObjectKey();
        if (string.IsNullOrEmpty(key))
        {
            if (logReason) Debug.LogWarning($"{spawnPoint.name} has no Object Key.", spawnPoint);
            return;
        }

        // Empty PoolObjectKey list = allow every key. Otherwise only listed keys spawn.
        if (PoolObjectKey.Count > 0 && !PoolObjectKey.Contains(key))
        {
            if (logReason) Debug.LogWarning($"Key '{key}' on {spawnPoint.name} is not in the player's Pool Object Key list.", spawnPoint);
            return;
        }

        Transform point = spawnPoint.transform;
        GameObject spawned = PoolEvents.Spawn(key, point.position, point.rotation);
        if (spawned == null)
        {
            if (logReason) Debug.LogWarning($"Pool returned nothing for '{key}' at {spawnPoint.name} (PoolController missing/disabled or still loading).", spawnPoint);
            return;
        }

        // Make it a child of the spawn point
        spawned.transform.SetParent(point, true);
        spawnPoint.SpawnedObject = spawned;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out SpawnPoints spawnPoint))
            return;

        GameObject spawned = spawnPoint.SpawnedObject;
        if (spawned == null)
            return;

        // Unparent so the pooled object isn't destroyed if the spawn point is destroyed
        spawned.transform.SetParent(null, true);
        PoolEvents.Despawn(spawnPoint.GetObjectKey(), spawned);
        spawnPoint.SpawnedObject = null;
    }
}
