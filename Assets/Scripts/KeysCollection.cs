using System;
using System.Collections.Generic;
using EasyUI.Toast;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static partial class Events
{
    public static event Action<int> OnKeyCount = null;
    public static void DoKeyCount(int Count) => OnKeyCount?.Invoke(Count);
    public static event Func<int> OnRemainingKey = null;
    public static int RemainingKey() => OnRemainingKey.Invoke();
    public static event Func<int> OnTotalKey = null;
    public static int TotalKeys() => OnRemainingKey.Invoke();
}
public class KeysCollection : MonoBehaviour
{
    bool win;
    private int RemainingKey;
    [SerializeField] int TotalKeys;
    [SerializeField] int CollectableKey;
    [SerializeField] TextMeshProUGUI KeyText;
    // [SerializeField] TextMeshProUGUI TotalKeyText;
    // [SerializeField] KeyIdentity[] keys;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Events.OnTotalKey += OnTotalKey;
        Events.OnKeyCount += OnKeyCount;
        Events.OnRemainingKey += OnRemaining;
    }

    private int OnTotalKey() => TotalKeys;

    private int OnRemaining() => RemainingKey;

    void Start()
    {
        CollectableKey = 0;
        // keys = FindObjectsOfType<KeyIdentity>();
    }

    // Update is called once per frame
    void Update()
    {
        // TotalKeyText.text = TotalKeys.ToString();
        KeyText.text = $"{CollectableKey} / {TotalKeys}";
        RemainingKey = TotalKeys - CollectableKey;
        if(CollectableKey >= TotalKeys && win == false)
        {
            Toast.Show("Congratulations you have Unlocked All Doors", 5f, ToastPosition.MiddleCenter);
            win = true;
        }
    }

    private void OnKeyCount(int value)
    {
        CollectableKey += value;
    }

    private void OnDisable()
    {
        Events.OnTotalKey -= OnTotalKey;
        Events.OnKeyCount -= OnKeyCount;
        Events.OnRemainingKey -= OnRemaining;
    }
}
