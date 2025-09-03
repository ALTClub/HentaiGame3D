using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyCharacteristics : MonoBehaviour
{
    [SerializeField] List<ExpCharacteristic> _characteristics;
    public ExpCharacteristic GetCharacteristic(string name) =>
        _characteristics.Find(c => c.Characteristic.name == name);
    Dictionary<string, int> saveData => _characteristics.ToDictionary(c => c.Characteristic.name, c => c.Experience);
    private void Start()
    {
        load();
        SaveManager.OnSave.AddListener(save);
    }
    public void AddExperience(string name, int value)
    {
        ExpCharacteristic characteristic = GetCharacteristic(name);
        characteristic.AddExperience(value);
        bool isNewLevel = characteristic.CheckNewLevel();
        if (CompareTag("Player"))
            ExpCharacteristic.OnGetExp.Invoke(characteristic, value, isNewLevel);
    }
    private void save()
    {
        SaveManager.UpdateSaveData(this, saveData, false);
    }
    private void load()
    {
        var dict = SaveManager.GetSaveData<Dictionary<string, int>>(this, false);
        if (dict == default) return;
        foreach (var c in _characteristics)
            c.Load(dict[c.Characteristic.name]);
    }
}
