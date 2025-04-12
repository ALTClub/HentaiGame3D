using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyCharacteristics : MonoBehaviour
{
    [SerializeField] protected List<Characteristic> _characteristics;
    public Dictionary<string, int> Experience = new();
    public Characteristic GetCharacteristic(string name) =>
        _characteristics.Find(c => c.name == name);
    private void Start()
    {
        foreach (var characteristic in _characteristics) characteristic.Init(this);
        load();
        SaveManager.OnSave.AddListener(save);
    }
    private void save()
    {
        string result = string.Join('\n', Experience);
        SaveManager.Save(this, result, false);
    }
    private void load()
    {
        string data = SaveManager.GetData(this, false);
        if (data == default) return;
        Experience = data.Split('\n').
            Select(pair => pair.Trim(new char[] { '[', ']' }).Split(',')).
            ToDictionary(splitpair => splitpair[0], splitpair => int.Parse(splitpair[1]));
    }
}
