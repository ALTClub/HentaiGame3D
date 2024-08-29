using System;
using UnityEngine.Events;

public static class TimeManager
{
    static DateTime dateTime = new(2023, 9, 1, 8, 0, 0);
    public static DateTime GetDateTime => dateTime;
    public static UnityEvent OnChangeTime = new();
    public static void AddTime(float minutes)
    {
        dateTime = dateTime.AddMinutes(minutes);
        OnChangeTime.Invoke();
    }
}
