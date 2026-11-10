using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using Unity.VisualScripting;
using UnityEngine;

public class DoorKeysManager : MonoBehaviour
{
    [SerializeField] List<KeyIdentity> keys = new List<KeyIdentity>();
    [SerializeField] List<DoorPhysics> doors = new List<DoorPhysics>();

    void Start()
    {
        GameObject[] KeysObject = GameObject.FindGameObjectsWithTag("Key");
        GameObject[] DoorsObject = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject key in KeysObject)
        {
            keys.Add(key.GetComponent<KeyIdentity>());
        }
        foreach (GameObject door in DoorsObject)
        {
            doors.Add(door.GetComponent<DoorPhysics>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < keys.Count; i++)
        {   
            if(keys[i].OnTrigger() == doors[i].DoorId)
            {   
                Debug.Log($"Door No {doors[i].DoorId} is Unlocked");
                doors[i].m_doorState = DoorState.Unlocked;
            }
        }
    }
}
