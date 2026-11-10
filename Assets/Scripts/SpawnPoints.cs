using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField] float m_radiusSize;
    [SerializeField] string m_objectKey;
    [SerializeField] SphereCollider sphereCollider;

    public string GetObjectKey() => m_objectKey;

    // Object currently spawned from the pool at this point (null if none)
    public GameObject SpawnedObject { get; set; }

    void Update()
    {
        if(sphereCollider.radius != m_radiusSize)
        {
            sphereCollider.radius = m_radiusSize;
        }
    }
}
