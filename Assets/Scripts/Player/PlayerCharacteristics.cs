using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] Characteristic[] characteristics;
    /// <summary>
    /// Возвращает характеристику с соответствующим именем
    /// </summary>
    public Characteristic GetCharacteristic(string name)
    {
        foreach (var characteristic in characteristics) if (characteristic.GetName == name) return characteristic;
        return null;
    }
}
