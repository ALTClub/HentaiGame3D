using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] CharacteristicsGroup[] _characteristicsGroups;
    Characteristic selected;
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
    public void GetCharacteristic(string name)
    {
        foreach (CharacteristicsGroup group in _characteristicsGroups)
            foreach (Characteristic characteristic in group.GetAllChars)
                if (characteristic.Name == name)
                {
                    selected = characteristic;
                    return;
                }
        selected = null;
    }
    private void Start()
    {
        Initialize();
        //GetGroup("Учёба").GetCharacteristic("Математика").Experience = 5;
    }
    void Initialize(int value = 0)
    {
        foreach (var group in _characteristicsGroups) group.Initialize();
    }
    public void AddExperience(int value)
    {
        if (selected == null) return;
        selected.Experience += value;
    }
}