using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] GameObject _savePrefab;
    [SerializeField] Button _continueButton;
    IEnumerable<Save> saves;
    private void Start()
    {
        UpdateButton();
        if (InputManager.Input == default) return;
        InputManager.Input.Player.QuickSave.performed += ctx => QuickSave();
        InputManager.Input.Player.QuickLoad.performed += ctx => quickLoad();
    }
    private void OnDisable()
    {
        if (InputManager.Input == default) return;
        InputManager.Input.Player.QuickSave.performed -= ctx => QuickSave();
        InputManager.Input.Player.QuickLoad.performed -= ctx => quickLoad();
    }
    public void RefreshSaves(Transform parent)
    {
        foreach (Transform t in parent)
            Destroy(t.gameObject);
        foreach (var save in saves)
            Instantiate(_savePrefab, parent).GetComponent<UISave>().Init(save);
    }
    public void UpdateButton()
    {
        saves = SaveManager.GetSaves();
        _continueButton.interactable = saves != null && saves.Count() > 0;
    }
    public void QuickSave() => SaveManager.Save(false);
    void quickLoad()
    {
        var saves = SaveManager.GetSaves();
        if (saves == default) return;
        SaveManager.Load(saves.First());
    }
}