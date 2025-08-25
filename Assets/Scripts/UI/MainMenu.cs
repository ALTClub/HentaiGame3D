using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _savePrefab;
    [SerializeField] Button _continueButton;
    void Awake()
    {
        SaveManager.root = Application.dataPath + @"\Saves\";
    }
    private void Start()
    {
        _continueButton.interactable = checkSaves();
    }
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void RefreshSaves(Transform parrent)
    {
        foreach (Transform t in parrent)
            Destroy(t.gameObject);
        if (!checkSaves()) return;
        foreach (var path in Directory.GetFiles(SaveManager.root))
        {
            if (path.Substring(path.Length - 4) != "json") continue;
            string json = File.ReadAllText(path);
            try
            {
                Dictionary<string, string> data = JsonUtility.FromJson<Serialization<string, string>>(json).ToDictionary();
                Instantiate(_savePrefab, parrent).GetComponent<UISave>().Init(data, path);
            }
            catch { }
        }
    }
    public void Exit() => Application.Quit();
    bool checkSaves() =>
        Directory.Exists(SaveManager.root) && Directory.GetFiles(SaveManager.root).Count() > 0;
}
