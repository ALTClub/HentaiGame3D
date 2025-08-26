using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _savePrefab;
    [SerializeField] Button _continueButton;
    private void Start()
    {
        _continueButton.interactable = SaveManager.CheckSaves();
    }
    public void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void RefreshSaves(Transform parrent)
    {
        foreach (Transform t in parrent)
            Destroy(t.gameObject);
        foreach (var save in SaveManager.GetSaves())
            Instantiate(_savePrefab, parrent).GetComponent<UISave>().Init(save.Value, save.Key);
    }
    public void Exit() => Application.Quit();
}
