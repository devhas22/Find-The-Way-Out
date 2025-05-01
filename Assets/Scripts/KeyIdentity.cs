using System;
using System.Collections;
using System.Collections.Generic;
using EasyUI.Toast;
using TMPro;
using UnityEngine;

public static partial class Events
{
    public static event Func<int> OnTrigger = null;
    public static int Dotrigger() => OnTrigger.Invoke();
    // public static event Action<int> OnKeyCount;
    // public static void DoKeyCount(int Count) => OnKeyCount?.Invoke(Count);
}
public class KeyIdentity : MonoBehaviour
{
    [SerializeField] sbyte Sbyte;
    [SerializeField] TextMeshProUGUI ProgressText;
    [SerializeField] public int KeyId;
    // [SerializeField] int TotalKeys;
    [SerializeField] DoorKeysManager doorKeysManager;

    private int OnTrigger() => KeyId;

    void Start()
    {
        Debug.Log(Sbyte);
        transform.tag = "Key";
        doorKeysManager = FindObjectOfType<DoorKeysManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Events.OnTrigger += OnTrigger;
            // int RemainingKey = TotalKeys--;
            Events.DoKeyCount(1);
            Toast.Show($"Door {KeyId} Has Unlocked", 4f);
            ProgressText.text = $"New Door {KeyId} Has Unlocked Remaining Keys {Events.TotalKeys() - 1}";
            Destroy(gameObject);
        }
    }
}
