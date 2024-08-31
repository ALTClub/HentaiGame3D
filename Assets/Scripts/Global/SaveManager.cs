using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class SaveManager
{
    public static UnityEvent OnSave = new();
    public static void Save(object sender, string data)
    {
        PlayerPrefs.SetString(sender.ToString(), data);
    }
    public static string GetData(object sender)
    {
        if (PlayerPrefs.HasKey(sender.ToString()))
            return PlayerPrefs.GetString(sender.ToString());
        else return null;
    }
}
