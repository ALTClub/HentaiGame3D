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
    public void RefreshSaves(Transform parrent) =>
        SaveManager.RefreshSaves(parrent, _savePrefab);
    public void Exit() => Application.Quit();
}
