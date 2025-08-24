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
    public void Init(Dictionary<string,string> saveData, string path)
    {
        data = saveData;
        this.path = path;
        _locationName.text = data["LocationName"];
        _saveTime.text = data["SaveTime"];
        _gameTime.text = data["DayCycleManager (TimeManager)"];
        _autosaveIndicator.SetActive(bool.Parse(data["IsAutosave"]));
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
