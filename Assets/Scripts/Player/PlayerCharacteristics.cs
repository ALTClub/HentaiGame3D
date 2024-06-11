using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] CharacteristicsGroup[] _characteristicsGroups;
    /// <summary>
    /// Возвращает группу характеристик с соответствующим именем
    /// </summary>
    public CharacteristicsGroup GetGroup(string name)
    {
        foreach (CharacteristicsGroup group in _characteristicsGroups)
            if (group.Name == name)
                return group;
        return null;
    }
    public CharacteristicsGroup[] GetAllGroups => _characteristicsGroups;
}
