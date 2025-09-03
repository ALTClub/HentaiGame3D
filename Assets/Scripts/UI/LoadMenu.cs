using UnityEngine;
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
        foreach (var save in SaveManager.GetSaves())
            Instantiate(_savePrefab, parent).GetComponent<UISave>().Init(save.Value, save.Key);
    }
    public void UpdateButton() =>
        _continueButton.interactable = SaveManager.CheckSaves();
}
