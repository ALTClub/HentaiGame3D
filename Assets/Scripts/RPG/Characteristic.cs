using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Characteistic")]
public class Characteristic : ScriptableObject
{
    [SerializeField] string _name, _definition;
    [SerializeField] Sprite _icon;

    [Header("Уровни владения")]
    [SerializeField] Level[] _levels;
    int _currentLevel = 0;
    int _experience;
    public string Name => _name;
    public string Definition => _definition;
    public Sprite Icon => _icon;
    public Level Level => _levels[_currentLevel];
    public int Experience
    {
        get => _experience;
        set
        {
            _experience = value;
            checkLevel();
        }
    }
    /// <summary>
    /// Проверка на возможность повышения уровня, в случае успеха повышает уровень до соответствующего опыту
    /// </summary>
    void checkLevel()
    {
        if (_currentLevel + 1 == _levels.Count()) return;
        if (_levels[_currentLevel + 1].valueToReach <= _experience) _currentLevel++;
        else return;
        checkLevel();
    }
    public float GetLevelProgress()
    {
        float sum = _levels[_currentLevel + 1].valueToReach - _levels[_currentLevel].valueToReach;
        float cur = _experience - _levels[_currentLevel].valueToReach;
        return cur / sum;
    }
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
