using UnityEngine;

[CreateAssetMenu(menuName = "Weather/Settings")]
public class WeatherSettings : ScriptableObject
{
    public int EarliestTime = 6, LatestTime = 18;
    public int MinRainDuration = 60, MaxRainDuration = 360;
    public int RainPerMonth = 12;
    public Material RainSkybox, Skybox;
}
