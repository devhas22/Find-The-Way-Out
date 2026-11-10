using System;
using UnityEngine;

namespace FYG.Pooling
{
    public static class Events
    {
        public static event Func<string, Vector3, Quaternion, GameObject> OnSpawn;
        public static GameObject Spawn(string tag, Vector3 position, Quaternion rotation) => OnSpawn?.Invoke(tag, position, rotation); 

        public static event Action<string, GameObject> OnDespawn;
        public static void Despawn(string tag, GameObject gameObject) => OnDespawn?.Invoke(tag, gameObject);
    }
}