using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] GameObject _savePrefab;
    [SerializeField] Button _continueButton;
    private void Start()
    {
        UpdateButton();
        InputManager.Input.Player.QuickSave.performed += ctx => QuickSave();
        InputManager.Input.Player.QuickLoad.performed += ctx => quickLoad();
    }
    private void OnDisable()
    {
        InputManager.Input.Player.QuickSave.performed -= ctx => QuickSave();
        InputManager.Input.Player.QuickLoad.performed -= ctx => quickLoad();
    }
    public void RefreshSaves(Transform parent)
    {

        foreach (Transform t in parent)
            Destroy(t.gameObject);
        foreach (var save in SaveManager.GetSaves())
            Instantiate(_savePrefab, parent).GetComponent<UISave>().Init(save.Value, save.Key);
    }
    public void UpdateButton() =>
        _continueButton.interactable = SaveManager.CheckSaves();
    public void QuickSave() => SaveManager.Save(false);
    void quickLoad()
    {
        var saves = SaveManager.GetSaves();
        if (saves == default || saves.Count == 0) return;
        SaveManager.Load(saves.First().Value);
    }
}