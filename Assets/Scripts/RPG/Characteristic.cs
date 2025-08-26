using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Characteistic")]
public class Characteristic : ScriptableObject
{
    [SerializeField] string _name, _definition;
    [SerializeField] Sprite _icon;
    [Header("Уровни владения")]
    [SerializeField] Level[] _levels;
    public string Name => _name;
    public string Definition => _definition;
    public Sprite Icon => _icon;
    public Level[] Levels => _levels;
}
[Serializable]
public class Level
{
    public string name;
    /// <summary>
    /// Сколько опыта необходимо для получения этого уровня
    /// </summary>
    public int valueToReach;
    [TextArea] public string description;
    public Color color;
}
