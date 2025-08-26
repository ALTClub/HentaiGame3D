using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyCharacteristics : MonoBehaviour
{
    [SerializeField] protected List<ExpCharacteristic> _characteristics;
    public ExpCharacteristic GetCharacteristic(string name) =>
        _characteristics.Find(c => c.Characteristic.name == name);
    Serialization<string, string> saveData => new(_characteristics.ToDictionary(c => c.Characteristic.name, c => c.Experience.ToString()));
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
        SaveManager.UpdateSaveData(this, JsonUtility.ToJson(saveData), false);
    }
    private void load()
    {
        string json = SaveManager.GetSaveData(this, false);
        if (json == default) return;
        var dict = JsonUtility.FromJson<Serialization<string, string>>(json).ToDictionary();
        foreach (var c in _characteristics)
            c.DeserializeExp(dict[c.Characteristic.name]);
    }
}
