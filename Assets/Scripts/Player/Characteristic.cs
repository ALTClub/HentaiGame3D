using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Characteistic")]
public class Characteristic : ScriptableObject
{
    [SerializeField] string Name;
    [SerializeField] string definition;
    public string GetName { get { return Name; } }
    public string GetDefinition { get { return definition; } }
    [SerializeField] Sprite icon;
    public Sprite GetIcon { get { return icon; } }

    [Header("Уровни владения")]
    [SerializeField] level[] levels;
    int currentLevel = 0;
    /// <summary>
    /// Текущий опыт владения хар-кой
    /// </summary>
    public level CurrentLevel { get { return levels[currentLevel]; } }
    int experience;
    public int Experience
    {
        get { return experience; }
        set
        {
            experience = value;
            checkLevel();
        }
    }
    /// <summary>
    /// Проверка на возможность повышения уровня, в случае успеха повышает уровень до соответствующего опыту
    /// </summary>
    void checkLevel()
    {
        if (currentLevel + 1 == levels.Count()) return;
        if (levels[currentLevel + 1].valueToReach <= experience) currentLevel++;
        else return;
        checkLevel();
    }
    public float GetLevelProgress()
    {
        float sum = levels[currentLevel + 1].valueToReach - levels[currentLevel].valueToReach;
        float cur = experience - levels[currentLevel].valueToReach;
        return cur / sum;
    }
}
[Serializable]
public struct level
{
    public string name;
    /// <summary>
    /// Сколько опыта необходимо для получения этого уровня
    /// </summary>
    public int valueToReach;
    [TextArea] public string description;
    public Color color;
}
