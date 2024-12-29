using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string _sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            LoadScene(_sceneToLoad);
    }
    public static void LoadScene(string scene)
    {
        SaveManager.OnSave.Invoke();
        SceneManager.LoadSceneAsync(scene);
    }

}
