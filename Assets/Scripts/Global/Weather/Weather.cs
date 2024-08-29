using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField] WeatherSettings Settings;
    ParticleSystem rainParticle;
    public static bool isRain { get; private set; }
    private void Start()
    {
        rainParticle = GetComponent<ParticleSystem>();
        Schedule.OnNewMonth.AddListener(onNewMonth);
        onNewMonth(); // Когда реализуем систему сохранений и смену сцен нужно будет переделать первый вызов
    }
    void onNewMonth()
    {
        DateTime current = TimeManager.GetDateTime;
        int daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);
        List<int> rainDays = new();
        while (rainDays.Count < Settings.RainPerMonth)
        {
            DateTime rainDay;
            do
            {
                rainDay = new(
                    current.Year,
                    current.Month,
                    UnityEngine.Random.Range(0, daysInMonth + 1),
                    UnityEngine.Random.Range(6, 22),
                    UnityEngine.Random.Range(0, 60),
                    UnityEngine.Random.Range(0, 60));
            }
            while (rainDays.Contains(rainDay.Day));
            rainDays.Add(rainDay.Day);
            TimeSpan rainDuration = TimeSpan.FromMinutes(UnityEngine.Random.Range(Settings.MinRainDuration, Settings.MaxRainDuration));
            Schedule.Task task = new("Rain", rainDay, rainDuration, new Action(startRain), new Action(stopRain));
            Schedule.AddTask(task);
        }
    }
    void startRain() => StartCoroutine(rain(true));
    void stopRain() => StartCoroutine(rain(false));
    IEnumerator rain(bool enabled)
    {
        ParticleSystem.MainModule main = rainParticle.main;
        Color color = main.startColor.color;
        if (enabled)
        {
            rainParticle.Play();
            isRain = true;
        }

        float t = 0, progress = 0;
        while (progress < 1f)
        {
            progress = Mathf.Lerp(0f, 1f, t / 8f);

            RenderSettings.skybox.Lerp(Settings.Skybox, Settings.RainSkybox, (enabled) ? progress : 1f - progress);
            DynamicGI.UpdateEnvironment();

            color.a = (enabled) ? progress : 1f - progress;
            color.a *= 0.75f;
            main.startColor = color;

            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (!enabled)
        {
            rainParticle.Stop();
            isRain = false;
        }
    }
}
