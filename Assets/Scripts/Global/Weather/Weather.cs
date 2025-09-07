using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField] WeatherSettings _settings;
    ParticleSystem rainParticle;
    static SaveData<List<Event>> rains;
    public static bool IsRain { get; private set; }
    Coroutine rain;
    private void Start()
    {
        rainParticle = GetComponent<ParticleSystem>();

        rains = new(this, true, new());
        if (rains.Value.Count == 0) scheduleRains();
        if (rains.Value.Any(rain => rain.Started))
            rain = StartCoroutine(rainCoroutine(true));

        //events
        TimeManager.OnNewMonth.AddListener(scheduleRains);
        TimeManager.OnChangeTime.AddListener(updateRains);
    }
    void scheduleRains()
    {
        rains.Value.Clear();
        while (rains.Value.Count < _settings.RainPerMonth)
        {
            DateTime rainDay = TimeManager.GetRandomDay(rains.Value.Select(rain => rain.Start.Day).ToArray(), _settings);
            TimeSpan rainDuration = TimeSpan.FromMinutes(UnityEngine.Random.Range(_settings.MinRainDuration, _settings.MaxRainDuration));

            Event rain = new(rainDay, rainDuration);
            rains.Value.Add(rain);
        }
    }
    void updateRains()
    {
        List<Event> completed = new();
        foreach (var rain in rains.Value)
        {
            TimeSpan diff = TimeManager.GetDateTime - rain.Start;
            if (diff <= TimeSpan.Zero) continue;
            if (diff > rain.Duration)
            {
                if (rain.Started)
                    this.rain = StartCoroutine(rainCoroutine(false));
                completed.Add(rain);
            }
            else if (!rain.Started)
            {
                this.rain = StartCoroutine(rainCoroutine(true));
                rain.Started = true;
            }
        }
        foreach (var rain in completed) rains.Value.Remove(rain);
    }
    IEnumerator rainCoroutine(bool enabled)
    {
        print($"Rain {enabled}");
        ParticleSystem.MainModule main = rainParticle.main;
        Color color = main.startColor.color;
        if (enabled)
        {
            rainParticle.Play();
            IsRain = true;
        }

        float t = 0, progress = 0;
        while (progress < 1f)
        {
            progress = Mathf.Lerp(0f, 1f, t / 8f);

            RenderSettings.skybox.Lerp(_settings.Skybox, _settings.RainSkybox, (enabled) ? progress : 1f - progress);
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
            IsRain = false;
        }
    }
}
