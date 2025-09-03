using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class UISave : MonoBehaviour
{
    [SerializeField] TMP_Text _locationName, _saveTime, _gameTime;
    [SerializeField] GameObject _autosaveIndicator;
    Dictionary<string, string> data;
    string path;
    T deserialize<T>(string Key) => JsonConvert.DeserializeObject<T>(data[Key]);
    public void Init(Dictionary<string, string> saveData, string path)
    {
        data = saveData;
        this.path = path;
        _locationName.text = deserialize<string>("LocationName");
        _saveTime.text = deserialize<DateTime>("SaveTime").ToString();
        _gameTime.text = deserialize<DateTime>("DayCycleManager (TimeManager)").ToString();
        _autosaveIndicator.SetActive(deserialize<bool>("IsAutosave"));
    }
    public void OnClick()
    {
        SaveManager.Load(data);
    }
    public void Delete()
    {
        File.Delete(path);
        Destroy(gameObject);
    }
}
