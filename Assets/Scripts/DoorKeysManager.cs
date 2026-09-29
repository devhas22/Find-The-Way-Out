using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using Unity.VisualScripting;
using UnityEngine;

public class DoorKeysManager : MonoBehaviour
{
    // [SerializeField] List<KeyIdentity> keys = new List<KeyIdentity>();
    [SerializeField] List<DoorPhysics> doors = new List<DoorPhysics>();

    void Start()
    {
        // GameObject[] KeysObject = GameObject.FindGameObjectsWithTag("Key");
        // GameObject[] DoorsObject = GameObject.FindGameObjectsWithTag("Door");

        // foreach (GameObject key in KeysObject)
        // {
        //     keys.Add(key.GetComponent<KeyIdentity>());
        // }
        // foreach (GameObject door in DoorsObject)
        // {
        //     doors.Add(door.GetComponent<DoorPhysics>());
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // for (int i = 1; i < doors.Count; i++)
        // {
        //     if(Events.Dotrigger() == 0) return;
            
        //     if(Events.Dotrigger() == doors[i].DoorId)
        //     {
        //         // Debug.Log($"Key {Events.Dotrigger()}");
                
        //         Debug.Log($"Door No {Events.Dotrigger()} is Unlocked");
        //         doors[i].m_doorState = DoorState.Unlocked;
        //     }
        // }
    }
}
