using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] Characteristic[] _characteristics;
    public Characteristic GetCharacteristic(string name)
    {
        foreach (Characteristic characteristic in _characteristics)
            if (characteristic.Name == name)
                return characteristic;
        return null;
    }
}