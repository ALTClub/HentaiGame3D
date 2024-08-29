using System;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] bool IsRealTime = false;
    [SerializeField] TMP_Text Clock;

    [SerializeField] Light Sun, Moon;
    [SerializeField] AnimationCurve SunCurve, MoonCurve;

    float sunIntensivity, moonIntensivity;
    void onChangeTime()
    {
        float min = Mathf.InverseLerp(0, 60, TimeManager.GetDateTime.Minute);
        DateTime decreased = TimeManager.GetDateTime.AddHours(18);
        float percentOfDay = Mathf.InverseLerp(0, 16, decreased.Hour + min) + Mathf.InverseLerp(16, 24, decreased.Hour + min);

        Sun.transform.localRotation = Quaternion.Euler(percentOfDay / 2 * 360f, 180f, 0);
        Moon.transform.localRotation = Quaternion.Euler(percentOfDay / 2 * 360f + 180f, 180f, 0);

        Sun.intensity = sunIntensivity * SunCurve.Evaluate(percentOfDay);
        Moon.intensity = moonIntensivity * MoonCurve.Evaluate(percentOfDay);
    }
    private void Start()
    {
        sunIntensivity = Sun.intensity;
        moonIntensivity = Moon.intensity;
        TimeManager.OnChangeTime.AddListener(onChangeTime);
    }
    private void Update()
    {
        Clock.text = TimeManager.GetDateTime.TimeOfDay.ToString("hh':'mm");
        if (!IsRealTime) return;
        TimeManager.AddTime(Time.deltaTime * 20f);
    }
}
