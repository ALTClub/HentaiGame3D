using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    static DateTime dateTime = new(2023, 9, 1, 8, 0, 0);
    public static DateTime GetDateTime => dateTime;

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
    public static int DaysInMonth() => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

    void Awake()
    {
        //load
        string saveData = SaveManager.GetData(this);
        if (saveData != null)
            dateTime = DateTime.Parse(saveData);
        mounth = dateTime.Month;

        //events
        SaveManager.OnSave.AddListener(save);
        OnChangeTime.AddListener(checkNewMonth);
    }

    public void save()
    {
        string data = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        SaveManager.Save(this, data);
    }
}
