using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarIdentity : MonoBehaviour
{
    [SerializeField] bool IsOn;
    [SerializeField] public int m_carId;
    [SerializeField] KeyCode m_headLightSwitch;
    [SerializeField] KeyCode m_exitCarKey;
    [SerializeField] PrometeoCarController m_carController;

    void Start()
    {
        m_carController = GetComponent<PrometeoCarController>();
    }

    void Update()
    {
        if (m_carController.enabled)
        {
            if (Input.GetKeyDown(m_exitCarKey))
            {
                Events.DoPersonActivate();
            }

            CarLights();
        }
    }

    private void CarLights()
    {
        if (Input.GetKeyDown(m_headLightSwitch))
        {
            IsOn = !IsOn;
        }

        if (!IsOn)
        {
            Events.DoCarLights(m_carId, false);
        }

        if (IsOn)
        {
            Events.DoCarLights(m_carId, true);
        }
    }
}
