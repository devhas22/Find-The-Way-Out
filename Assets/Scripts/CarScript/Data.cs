using System;
using UnityEngine;

[Serializable]
public struct CarInfo
{
    public int CarId;
    public GameObject Car;
    public Light[] CarLights;
    public Transform DoorPosition;
    public PrometeoCarController carController;
}

[Serializable]
public struct PersonInfo
{
    public GameObject Player;
    public PlayerController playerController;
}
