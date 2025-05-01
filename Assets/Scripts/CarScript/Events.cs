using System;
public static partial class Events
{
    public static event Action<int,bool> CarLights;
    public static event Action PersonActivate;
    public static event Action<int> CarActivate;
    public static event Action PersonDeActivate;
    public static event Func<bool> IsCarActivate;
    public static void DoCarLights(int id, bool lightSwitch) => CarLights?.Invoke(id, lightSwitch);
    public static bool DoIsCarActivate() => IsCarActivate.Invoke();
    public static void DoPersonActivate() => PersonActivate?.Invoke();
    public static void DoCarActivate(int id) => CarActivate?.Invoke(id);
    public static void DoPersonDeActivate() => PersonDeActivate?.Invoke();
}
