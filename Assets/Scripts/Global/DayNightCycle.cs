using System;
using TMPro;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] bool _isRealTime = false;
    [SerializeField] TMP_Text _clock;

    [SerializeField] Light _sun, _moon;
    [SerializeField] AnimationCurve _sunCurve, _moonCurve;

    float _sunIntensivity, _moonIntensivity;
    private void Start()
    {
        _sunIntensivity = _sun.intensity;
        _moonIntensivity = _moon.intensity;
        TimeManager.OnChangeTime.AddListener(setSunMoonRotation);
        TimeManager.OnChangeTime.AddListener(updateClock);
        updateClock();
    }
    void updateClock() => _clock.text = TimeManager.GetDateTime.TimeOfDay.ToString("hh':'mm");
    void setSunMoonRotation()
    {
        float min = Mathf.InverseLerp(0, 60, TimeManager.GetDateTime.Minute);
        DateTime decreased = TimeManager.GetDateTime.AddHours(18);
        float percentOfDay = Mathf.InverseLerp(0, 16, decreased.Hour + min) + Mathf.InverseLerp(16, 24, decreased.Hour + min);

        _sun.transform.localRotation = Quaternion.Euler(percentOfDay / 2 * 360f, 180f, 0);
        _moon.transform.localRotation = Quaternion.Euler(percentOfDay / 2 * 360f + 180f, 180f, 0);

        _sun.intensity = _sunIntensivity * _sunCurve.Evaluate(percentOfDay);
        _moon.intensity = _moonIntensivity * _moonCurve.Evaluate(percentOfDay);
    }
    private void Update()
    {
        if (!_isRealTime) return;
        TimeManager.AddTime(Time.deltaTime * 20f);
    }
}
