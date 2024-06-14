using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] CharacteristicsGroup[] _characteristicsGroups;
    Characteristic _selected;
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
    public void SelectCharacteristic(string name)
    {
        foreach (CharacteristicsGroup group in _characteristicsGroups)
            foreach (Characteristic characteristic in group.GetAllChars)
                if (characteristic.Name == name)
                {
                    _selected = characteristic;
                    return;
                }
        _selected = null;
    }
    public void AddExperience(int value)
    {
        if (_selected == null) return;
        _selected.Experience += value;
    }
}