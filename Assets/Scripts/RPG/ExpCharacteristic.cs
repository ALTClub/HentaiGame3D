using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ExpCharacteristic
{
    [SerializeField] Characteristic _characteristic;
    public Characteristic Characteristic => _characteristic;

    public static UnityEvent<ExpCharacteristic, int, bool> OnGetExp = new();
    public int Experience { get; private set; }
    public void DeserializeExp(string value)
    {
        Experience = int.Parse(value);
        oldLavel = current;
    }
    public Level Level => oldLavel == default ? _characteristic.Levels[0] : oldLavel;
    Level oldLavel;
    public void AddExperience(int value) => Experience += value;
    public bool CheckNewLevel()
    {
        bool isNew = Level.name != current.name;
        if (isNew) oldLavel = current;
        return isNew;
    }
    public float GetLevelProgress =>
        Mathf.InverseLerp(Level.valueToReach, next.valueToReach, Experience);
    public bool CheckLevel(string level) =>
        _characteristic.Levels.ToList().Any(l => l.name == level && l.valueToReach <= Experience);
    public bool CheckLevel(int level) =>
        _characteristic.Levels[level].valueToReach <= Experience;
    Level current => _characteristic.Levels.LastOrDefault(level => level.valueToReach <= Experience);
    Level next => _characteristic.Levels.FirstOrDefault(level => level.valueToReach > Experience);
}
