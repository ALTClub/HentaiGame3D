using Newtonsoft.Json;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] GameObject _savePrefab;
    [SerializeField] Button _continueButton;
    private void OnEnable()
    {
        UpdateButton();
    }
    public void RefreshSaves(Transform parent)
    {

        foreach (Transform t in parent)
            Destroy(t.gameObject);
        var saves = SaveManager.GetSaves().OrderByDescending(kv => SaveManager.Deserialize<DateTime>(kv.Value, "SaveTime"))
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        foreach (var save in saves)
            Instantiate(_savePrefab, parent).GetComponent<UISave>().Init(save.Value, save.Key);
    }
    public void UpdateButton() =>
        _continueButton.interactable = SaveManager.CheckSaves();
}
