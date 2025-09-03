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
    public void Init(Dictionary<string, string> saveData, string path)
    {
        data = saveData;
        this.path = path;
        _locationName.text = SaveManager.Deserialize<string>(data, "LocationName");
        _saveTime.text = SaveManager.Deserialize<DateTime>(data, "SaveTime").ToString();
        _gameTime.text = SaveManager.Deserialize<DateTime>(data, "DayCycleManager (TimeManager)").ToString();
        _autosaveIndicator.SetActive(SaveManager.Deserialize<bool>(data, "IsAutosave"));
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
