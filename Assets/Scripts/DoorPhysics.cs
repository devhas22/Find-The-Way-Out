using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using TMPro;
using UnityEngine;

public enum DoorState
{
    Locked, Unlocked
}
public class DoorPhysics : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NumberText;
    [SerializeField] public Rigidbody m_rigidbody;
    [SerializeField] public int DoorId;
    [SerializeField] public DoorState m_doorState;
    void Start()
    {
        transform.tag = "Door";
        // NumberText.text = DoorId.ToString();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Physics.IgnoreLayerCollision(3, 6);

        Locked();
        Unlocked();
    }

    public void Locked()
    {
        if (m_doorState == DoorState.Locked) m_rigidbody.isKinematic = true;
    }
    public void Unlocked()
    {
        if (m_doorState == DoorState.Unlocked) m_rigidbody.isKinematic = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            if(this.m_doorState == DoorState.Locked)
            {
                Debug.Log("Door Has Locked");
                Toast.Show("Door Has Locked", 1f);
            }
        }
    }
}
