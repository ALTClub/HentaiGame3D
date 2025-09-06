using System.IO;
using TMPro;
using UnityEngine;

public class UISave : MonoBehaviour
{
    [SerializeField] TMP_Text _locationName, _saveTime, _gameTime;
    [SerializeField] GameObject _autosaveIndicator;
    Save save;
    public void Init(Save saveData)
    {
        save = saveData;
        _locationName.text = save.LocationName;
        _saveTime.text = save.SaveTime.ToString();
        _gameTime.text = save.GameTime.ToString();
        _autosaveIndicator.SetActive(save.IsAutosave);
    }
    public void OnClick()
    {
        SaveManager.Load(save);
    }
    public void Delete()
    {
        File.Delete(save.Path);
        Destroy(gameObject);
    }
}
