using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CarManagement : MonoBehaviour
{
    [SerializeField] bool IsPerson, IsCar;
    [SerializeField] CarInfo m_currentCar;
    [SerializeField] PersonInfo m_personInfo;
    [SerializeField] List<CarInfo> m_carInfo;
    [SerializeField] CinemachineVirtualCamera m_cinemachineVirtualCamera;

    public void OnEnable()
    {
        Events.CarLights += CarLights;
        Events.CarActivate += CarActivate;
        Events.IsCarActivate += IsCarActivate;
        Events.PersonActivate += PersonActivate;
        Events.PersonDeActivate += PersonDeactivate;
    }

    private void CarLights(int id, bool lightSwitch)
    {
        foreach (var light in m_carInfo[id].CarLights)
        {
            light.enabled = lightSwitch;
        }
    }

    public void OnDisable()
    {
        Events.CarActivate -= CarActivate;
        Events.IsCarActivate -= IsCarActivate;
        Events.PersonActivate -= PersonActivate;
        Events.PersonDeActivate -= PersonDeactivate;
    }

    private bool IsCarActivate() => IsCar;

    private void CarActivate(int id)
    {
        if (IsCar && !IsPerson)
        {

            Debug.Log(id);
            Debug.Log("Car Activate");
            m_currentCar = m_carInfo[id];
            m_currentCar.Car.tag = "Player";
            m_carInfo[id].carController.enabled = true;
            m_cinemachineVirtualCamera.Follow = m_currentCar.Car.transform;
            m_cinemachineVirtualCamera.LookAt = m_currentCar.Car.transform;
            Events.DoCarLights(id, true);
        }
    }

    private void PersonActivate()
    {
        Debug.Log("Person Active");
        IsCar = false;
        IsPerson = true;
        m_currentCar.Car.tag = "Public";
        m_currentCar.CarId = 0;
        m_currentCar.Car = null;
        m_currentCar.carController = null;
        m_personInfo.Player.SetActive(true);
        m_personInfo.playerController.enabled = true;
        m_cinemachineVirtualCamera.Follow = m_personInfo.Player.transform;
        m_cinemachineVirtualCamera.LookAt = m_personInfo.Player.transform;
        m_personInfo.playerController.transform.position = m_currentCar.DoorPosition.position;

        foreach (CarInfo carInfo in m_carInfo)
        {
            carInfo.carController.enabled = false;
        }
    }

    private void PersonDeactivate()
    {
        Debug.Log("Person DeActive");
        IsCar = true;
        IsPerson = false;
        m_personInfo.Player.SetActive(false);
        m_personInfo.playerController.enabled = false;
    }
}
