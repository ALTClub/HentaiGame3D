using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static UnityEvent OnSave = new();
    public static void Save(object sender, string data)
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + ": " + sender.ToString(), data);
    }
    public static string GetData(object sender)
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + ": " + sender.ToString()))
            return PlayerPrefs.GetString(SceneManager.GetActiveScene().name + ": " + sender.ToString());
        else return null;
    }
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
