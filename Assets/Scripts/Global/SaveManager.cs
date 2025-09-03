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
        UpdateSaveData("SaveTime", DateTime.Now, false);
        UpdateSaveData("IsAutosave", isAutosave, false);
        string json = JsonConvert.SerializeObject(saveData);
        if (!Directory.Exists(root))
            Directory.CreateDirectory(root);
        File.WriteAllText(root + $"Save{DateTime.Now:dd-MM-yyyy-HH-mm}.json", json);
    }
    public static void Load(Dictionary<string, string> data)
    {
        saveData = data;
        SceneManager.LoadSceneAsync(GetSaveData<string>("SceneName", false));
        Time.timeScale = 1;
    }
    public static Dictionary<string, Dictionary<string, string>> GetSaves()
    {
        if (!CheckSaves()) return null;
        var paths = Directory.GetFiles(root).Where(path => path.Substring(path.Length - 4) == "json");
        return paths.ToDictionary(path => path, path =>
        JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path)));
    }
    public static bool CheckSaves() =>
        Directory.Exists(root) && Directory.GetFiles(root).Count() > 0;
    public static void Reset() => saveData = new();
}
