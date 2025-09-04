using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void NewGame()
    {
        SaveManager.Reset();
        SceneManager.LoadSceneAsync(1);
    }
    public void Exit() => Application.Quit();
}
