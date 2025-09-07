using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static UnityEvent OnSave = new();
    static Dictionary<string, string> saveData = new();
    public static string root => Application.dataPath + @"\Saves\";
    public static void UpdateSaveData<T>(object sender, T data, bool UseSceneID = true)
    {
        string key = (UseSceneID ? SceneManager.GetActiveScene().name + ": " : "") + sender.ToString();
        saveData[key] = JsonConvert.SerializeObject(data);
    }
    public static T GetSaveData<T>(object sender, bool UseSceneID = true)
    {
        string key = (UseSceneID ? SceneManager.GetActiveScene().name + ": " : "") + sender.ToString();
        if (saveData.ContainsKey(key))
            return JsonConvert.DeserializeObject<T>(saveData[key]);
        else return default;
    }
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
    public static void Save(bool isAutosave)
    {
        OnSave.Invoke();
        var save = new Save(isAutosave, saveData);
        if (!Directory.Exists(root))
            Directory.CreateDirectory(root);
        File.WriteAllText(save.Path, JsonConvert.SerializeObject(save));
    }
    public static void Load(Save save)
    {
        saveData = save.SaveData;
        SceneManager.LoadSceneAsync(save.SceneName);
        Time.timeScale = 1;
    }
    public static IEnumerable<Save> GetSaves()
    {
        if (!Directory.Exists(root)) return null;
        var paths = Directory.GetFiles(root).Where(path => path.EndsWith(".json"));
        var saves = paths.Select(path =>
        {
            try { return (Save?)JsonConvert.DeserializeObject<Save>(File.ReadAllText(path)); }
            catch { return null; }
        })
        .Where(save => save.HasValue).Select(save => save.Value);
        var sorted = saves.OrderByDescending(save => save.SaveTime);
        return sorted;
    }
    public static void Reset() => saveData = new();
}
public struct Save
{
    public string Path, LocationName, SceneName;
    public DateTime SaveTime, GameTime;
    public bool IsAutosave;
    public Dictionary<string, string> SaveData;
    public Save(bool isAutosave, Dictionary<string, string> saveData)
    {
        Path = SaveManager.root + $"Save{DateTime.Now:dd-MM-yyyy-HH-mm}.json";
        LocationName = Location.Current;
        SceneName = SceneManager.GetActiveScene().name;
        SaveTime = DateTime.Now;
        GameTime = TimeManager.GetDateTime;
        IsAutosave = isAutosave;
        SaveData = saveData;
    }
}
[Serializable]
public class SaveData<T>
{
    public T Value;
    string sender;
    bool isGlobal;
    public SaveData(object sender, bool isGlobal, T defaultValue)
    {
        this.sender = sender.ToString();
        this.isGlobal = isGlobal;
        var data = SaveManager.GetSaveData<T>(sender, !isGlobal);
        Value = data == null || data.Equals(default(T)) ? defaultValue : data;
        SaveManager.OnSave.AddListener(save);
    }
    void save()
    {
        SaveManager.UpdateSaveData<T>(sender, Value, !isGlobal);
    }
}
