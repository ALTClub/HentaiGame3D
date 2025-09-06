using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyCharacteristics : MonoBehaviour
{
    [SerializeField] List<Characteristic> _characteristics;
    public ExpCharacteristic GetCharacteristic(string name) =>
        characteristics.Value.Find(c => c.Name == name);
    SaveData<List<ExpCharacteristic>> characteristics;
    private void Awake()
    {
        characteristics = new(this, true, _characteristics.Select(c => new ExpCharacteristic(c)).ToList());
        var bad = characteristics.Value.Where(t => t.Characteristic == null);
        if (bad.Count() != 0)
            characteristics.Value = bad.Select(ec => new ExpCharacteristic(_characteristics.Find(_c => _c.name == ec.Name), ec.Experience))
                .ToList().Concat(characteristics.Value).ToList();
    }
    public void AddExperience(string name, int value)
    {
        ExpCharacteristic characteristic = GetCharacteristic(name);
        characteristic.AddExperience(value);
        bool isNewLevel = characteristic.CheckNewLevel();
        if (CompareTag("Player"))
            ExpCharacteristic.OnGetExp.Invoke(characteristic, value, isNewLevel);
    }
}
