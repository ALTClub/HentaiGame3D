using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    static DateTime dateTime = new(2023, 9, 1, 8, 0, 0);
    public static DateTime GetDateTime => dateTime;
    public static DateTime TodayInTime(TimeSpan target) =>
        new(dateTime.Year, dateTime.Month, dateTime.Day, target.Hours, target.Minutes, target.Seconds);

    public static UnityEvent OnChangeTime = new();
    public static UnityEvent OnNewMonth = new();

    static int mounth;
    static void checkNewMonth()
    {
        if (GetDateTime.Month == mounth) return;

        OnNewMonth.Invoke();
        mounth = dateTime.Month;
    }
    public static void AddTime(float minutes)
    {
        dateTime = dateTime.AddMinutes(minutes);
        OnChangeTime.Invoke();
    }
    public static bool IsWeekend => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    public static int DaysInMonth() => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    public static DateTime GetRandomDay(int[] daysBlackList, WeatherSettings _settings)
    {
        int newDay;
        do newDay = UnityEngine.Random.Range(1, DaysInMonth() + 1);
        while (daysBlackList.Contains(newDay));

        int hour = UnityEngine.Random.Range(_settings.EarliestTime, _settings.LatestTime);
        int minute = UnityEngine.Random.Range(0, 60);
        int second = UnityEngine.Random.Range(0, 60);

        return new(GetDateTime.Year, GetDateTime.Month, newDay, hour, minute, second);
    }

    void Awake()
    {
        //load
        var data = SaveManager.GetSaveData<DateTime>(this, false);
        if (data != default) dateTime = data;
        mounth = dateTime.Month;

        //events
        SaveManager.OnSave.AddListener(save);
        OnChangeTime.AddListener(checkNewMonth);
    }

    public void save() => SaveManager.UpdateSaveData(this, dateTime, false);
}
