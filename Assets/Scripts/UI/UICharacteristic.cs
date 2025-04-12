using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICharacteristic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _progress, _icon, _levelBG;
    [SerializeField] TMP_Text _name, _level;
    [SerializeField] GameObject _hintPrefab;
    [SerializeField] MyCharacteristics _playerCharacteristics;
    Level _currentLevel;
    string _difinition;
    GameObject _spawnedHint;
    Transform _parent;
    void setCharacteristic(Characteristic characteristic)
    {
        if (characteristic == null) return;
        _parent = transform.parent;
        _icon.sprite = characteristic.Icon;
        _name.text = characteristic.Name;
        _levelBG.color = characteristic.Level.color;
        _level.text = characteristic.Level.name;
        _difinition = characteristic.Definition;
        _currentLevel = characteristic.Level;
        _progress.fillAmount = characteristic.GetLevelProgress();
    }
    private void OnEnable()
    {
        setCharacteristic(_playerCharacteristics.GetCharacteristic(gameObject.name));
    }
    private void OnDisable()
    {
        Destroy(_spawnedHint?.gameObject);
        _spawnedHint = null;
    }
    private void Update()
    {
        if (_spawnedHint != null)
            _spawnedHint.transform.position = Input.mousePosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _spawnedHint = Instantiate(_hintPrefab, transform.position, Quaternion.identity);
        _spawnedHint.transform.SetParent(_parent);
        foreach (Transform t in _spawnedHint.transform)
        {
            TMP_Text text = t.GetComponent<TMP_Text>();
            if (t.name == "Name") text.text = _difinition;
            if (t.name == "Description") text.text = _currentLevel.description;
        }
    }
    public void OnPointerExit(PointerEventData eventData) => OnDisable();
}
