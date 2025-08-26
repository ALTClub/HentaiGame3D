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
    public static void UpdateSaveData(object sender, string data, bool UseSceneID = true)
    {
        string key = (UseSceneID ? SceneManager.GetActiveScene().name + ": " : "") + sender.ToString();
        saveData[key] = data;
    }
    public static string GetSaveData(object sender, bool UseSceneID = true)
    {
        string key = (UseSceneID ? SceneManager.GetActiveScene().name + ": " : "") + sender.ToString();
        if (saveData.ContainsKey(key))
            return saveData[key];
        else return null;
    }
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
    public static void Save(bool isAutosave)
    {
        OnSave.Invoke();
        UpdateSaveData("SaveTime", DateTime.Now.ToString(), false);
        UpdateSaveData("IsAutosave", isAutosave.ToString(), false);
        string json = JsonUtility.ToJson(new Serialization<string, string>(saveData));
        if (!Directory.Exists(root))
            Directory.CreateDirectory(root);
        File.WriteAllText(root + $"Save{DateTime.Now:dd-MM-yyyy-HH-mm}.json", json);
    }
    public static void Load(Dictionary<string, string> data)
    {
        saveData = data;
        SceneManager.LoadSceneAsync(saveData["SceneName"]);
        Time.timeScale = 1;
    }
    public static Dictionary<string, Dictionary<string, string>> GetSaves()
    {
        if (!CheckSaves()) return null;
        var paths = Directory.GetFiles(root).Where(path => path.Substring(path.Length - 4) == "json");
        return paths.ToDictionary(path => path, path =>
        JsonUtility.FromJson<Serialization<string, string>>(File.ReadAllText(path)).ToDictionary());

    }
    public static bool CheckSaves() =>
        Directory.Exists(root) && Directory.GetFiles(root).Count() > 0;
}
