using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Characteistic")]
public class Characteristic : ScriptableObject
{
    [SerializeField] string _name, _definition;
    [SerializeField] Sprite _icon;

    [Header("Уровни владения")]
    [SerializeField] Level[] _levels;
    public static UnityEvent<Characteristic, int> OnGetExp = new();
    MyCharacteristics owner;
    public string Name => _name;
    public string Definition => _definition;
    public Sprite Icon => _icon;
    public Level Level => _levels.LastOrDefault(level => level.valueToReach <= Experience);
    public int Experience
    {
        get => owner.Experience[name];
        set
        {
            owner.Experience[name] = value;
        }
    }
    public void AddExperience(int value)
    {
        Experience += value;
        if (owner.CompareTag("Player")) OnGetExp.Invoke(this, value);
    }
    public void Init(MyCharacteristics owner)
    {
        this.owner = owner;
        owner.Experience[name] = 0;
    }
    public float GetLevelProgress()
    {
        float sum = _levels.FirstOrDefault(level => level.valueToReach > Experience).valueToReach - Level.valueToReach;
        float cur = Experience - Level.valueToReach;
        return cur / sum;
    }
    public bool CheckLevel(string level) =>
        _levels.ToList().Any(l => l.name == level && l.valueToReach <= Experience);
    public bool CheckLevel(int level) =>
        _levels[level].valueToReach <= Experience;
}
[Serializable]
public struct Level
{
    public string name;
    /// <summary>
    /// Сколько опыта необходимо для получения этого уровня
    /// </summary>
    public int valueToReach;
    [TextArea] public string description;
    public Color color;
}
