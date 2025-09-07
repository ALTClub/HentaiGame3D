using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    static SaveData<DateTime> dateTime;
    public static DateTime GetDateTime => dateTime.Value;
    public static DateTime TodayInTime(TimeSpan target) =>
        new(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, target.Hours, target.Minutes, target.Seconds);

    public static UnityEvent OnChangeTime = new();
    public static UnityEvent OnNewMonth = new();

    static int mounth;
    static void checkNewMonth()
    {
        if (GetDateTime.Month == mounth) return;

        OnNewMonth.Invoke();
        mounth = dateTime.Value.Month;
    }
    public static void AddTime(float minutes)
    {
        dateTime.Value = dateTime.Value.AddMinutes(minutes);
        OnChangeTime.Invoke();
    }
    public static bool IsWeekend => dateTime.Value.DayOfWeek == DayOfWeek.Saturday || dateTime.Value.DayOfWeek == DayOfWeek.Sunday;
    public static int DaysInMonth() => DateTime.DaysInMonth(dateTime.Value.Year, dateTime.Value.Month);
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
        dateTime = new(this, true, new(2023, 9, 1, 8, 0, 0));
        mounth = dateTime.Value.Month;

        //events
        OnChangeTime.AddListener(checkNewMonth);
    }
}
