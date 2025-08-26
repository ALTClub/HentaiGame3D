using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField] WeatherSettings _settings;
    ParticleSystem rainParticle;
    static List<Event> rains = new();
    public static bool IsRain { get; private set; }
    Coroutine rain;
    private void Start()
    {
        rainParticle = GetComponent<ParticleSystem>();

        if (!load()) rainSchedule();

        //events
        TimeManager.OnNewMonth.AddListener(rainSchedule);
        TimeManager.OnChangeTime.AddListener(checkRain);
        SaveManager.OnSave.AddListener(save);
    }
    private bool load()
    {
        string saveData = SaveManager.GetSaveData(this, false);
        if (saveData == default) return false;

        rains.AddRange(saveData.Split('\n').Select(serialized => new Event(serialized)));
        if (rains.Any(rain => rain.Started))
            rain = StartCoroutine(rainCoroutine(true));

        return true;
    }
    void save()
    {
        if (rains.Count <= 0) return;
        List<string> saveData = new();
        foreach (var rain in rains) saveData.Add(rain.Serialize());
        SaveManager.UpdateSaveData(this, string.Join('\n', saveData.ToArray()), false);
    }
    void rainSchedule()
    {
        rains.Clear();
        while (rains.Count < _settings.RainPerMonth)
        {
            DateTime rainDay = TimeManager.GetRandomDay(rains.Select(rain => rain.StartDate.Day).ToArray(), _settings);
            TimeSpan rainDuration = TimeSpan.FromMinutes(UnityEngine.Random.Range(_settings.MinRainDuration, _settings.MaxRainDuration));

            Event rain = new(rainDay, rainDuration);
            rains.Add(rain);
        }
    }
    void checkRain()
    {
        List<Event> completed = new();
        foreach (var rain in rains)
        {
            TimeSpan diff = TimeManager.GetDateTime - rain.StartDate;
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
        foreach (var rain in completed) rains.Remove(rain);
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
