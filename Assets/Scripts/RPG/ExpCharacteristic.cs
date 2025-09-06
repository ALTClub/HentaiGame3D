using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ExpCharacteristic
{
    [JsonIgnore] public Characteristic Characteristic;
    public string Name { get; private set; }

    [JsonIgnore] public static UnityEvent<ExpCharacteristic, int, bool> OnGetExp = new();
    public int Experience { get; private set; }
    [JsonConstructor]
    public ExpCharacteristic(string Name, int Experience) { this.Name = Name; this.Experience = Experience; }
    public ExpCharacteristic(Characteristic characteristic, int exp = 0)
    {
        Characteristic = characteristic;
        Name = characteristic.name;
        Experience = exp;
        oldLevel = current;
    }
    [JsonIgnore] public Level Level => oldLevel == default ? Characteristic.Levels[0] : oldLevel;
    Level oldLevel;
    public void AddExperience(int value) => Experience += value;
    public bool CheckNewLevel()
    {
        bool isNew = Level.name != current.name;
        if (isNew) oldLevel = current;
        return isNew;
    }
    [JsonIgnore]
    public float GetLevelProgress =>
        Mathf.InverseLerp(Level.valueToReach, next.valueToReach, Experience);
    public bool CheckLevel(string level) =>
        Characteristic.Levels.ToList().Any(l => l.name == level && l.valueToReach <= Experience);
    public bool CheckLevel(int level) =>
        Characteristic.Levels[level].valueToReach <= Experience;
    Level current => Characteristic.Levels.LastOrDefault(level => level.valueToReach <= Experience);
    Level next => Characteristic.Levels.FirstOrDefault(level => level.valueToReach > Experience);
}
