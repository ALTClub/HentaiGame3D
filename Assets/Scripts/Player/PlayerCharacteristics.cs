using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] CharacteristicGroup[] CharacteristicGroups;
    /// <summary>
    /// Возвращает группу характеристик с соответствующим именем
    /// </summary>
    public CharacteristicGroup GetGroup(string name)
    {
        foreach (CharacteristicGroup group in CharacteristicGroups) if (group.GetName == name) return group;
        return null;
    }
    public CharacteristicGroup[] GetAllGroups { get { return CharacteristicGroups; } }
}
[Serializable]
public class CharacteristicGroup
{
    [SerializeField] string name;
    public string GetName { get { return name; } }
    [SerializeField] Characteristic[] characteristics;
    public Characteristic[] GetAllChars { get { return characteristics; } }
    /// <summary>
    /// Возвращает характеристику с соответствующим именем
    /// </summary>
    public Characteristic GetCharacteristic(string name)
    {
        foreach (Characteristic characteristic in characteristics) if (characteristic.GetName == name) return characteristic;
        return null;
    }
}
