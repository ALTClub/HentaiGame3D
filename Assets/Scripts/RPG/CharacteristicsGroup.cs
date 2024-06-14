using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacteristicsGroup
{
    [SerializeField] string _name;
    [SerializeField] Characteristic[] _characteristics;
    public string Name => _name;
    public Characteristic[] GetAllChars => _characteristics;
    /// <summary>
    /// Возвращает характеристику с соответствующим именем
    /// </summary>
    public Characteristic GetCharacteristic(string name)
    {
        foreach (Characteristic characteristic in _characteristics)
            if (characteristic.Name == name)
                return characteristic;
        return null;
    }
    public void Initialize()
    {
        foreach (var characteristic in _characteristics) characteristic.Initialize();
    }
}
