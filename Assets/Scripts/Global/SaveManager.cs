using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static UnityEvent OnSave = new();
    public static void Save(object sender, string data, bool UseSceneID = true)
    {
        string key = (UseSceneID ? SceneManager.GetActiveScene().name + ": " : "") + sender.ToString();
        PlayerPrefs.SetString(key, data);
    }
    public static string GetData(object sender, bool UseSceneID = true)
    {
        string key = (UseSceneID ? SceneManager.GetActiveScene().name + ": " : "") + sender.ToString();
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetString(key);
        else return null;
    }
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
