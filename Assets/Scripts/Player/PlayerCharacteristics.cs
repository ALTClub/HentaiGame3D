using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] List<Characteristic> _characteristics;
    public Characteristic GetCharacteristic(string name) =>
        _characteristics.Find(c => c.name == name);
}