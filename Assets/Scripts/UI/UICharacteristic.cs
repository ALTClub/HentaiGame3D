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
    Level currentLevel;
    string difinition;
    GameObject spawnedHint;
    Transform parent;
    void setCharacteristic(ExpCharacteristic c)
    {
        if (c == null) return;
        parent = transform.parent;
        _icon.sprite = c.Characteristic.Icon;
        _name.text = c.Characteristic.Name;
        _levelBG.color = c.Level.color;
        _level.text = c.Level.name;
        difinition = c.Characteristic.Definition;
        currentLevel = c.Level;
        _progress.fillAmount = c.GetLevelProgress;
    }
    private void OnEnable()
    {
        setCharacteristic(_playerCharacteristics.GetCharacteristic(gameObject.name));
    }
    private void OnDisable()
    {
        Destroy(spawnedHint?.gameObject);
        spawnedHint = null;
    }
    private void Update()
    {
        if (spawnedHint != null)
            spawnedHint.transform.position = Input.mousePosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        spawnedHint = Instantiate(_hintPrefab, transform.position, Quaternion.identity);
        spawnedHint.transform.SetParent(parent);
        foreach (Transform t in spawnedHint.transform)
        {
            TMP_Text text = t.GetComponent<TMP_Text>();
            if (t.name == "Name") text.text = difinition;
            if (t.name == "Description") text.text = currentLevel.description;
        }
    }
    public void OnPointerExit(PointerEventData eventData) => OnDisable();
}
