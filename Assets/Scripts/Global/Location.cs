using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Location : MonoBehaviour
{
    public string Name;
    
    void Start()
    {
        SaveManager.OnSave.AddListener(save);
    }
    void save()
    {
        SaveManager.UpdateSaveData("LocationName", Name, false);
        SaveManager.UpdateSaveData("SceneName", SceneManager.GetActiveScene().name, false);
    }
}
