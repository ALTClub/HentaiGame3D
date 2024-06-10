using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UICharacteristic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image progress, Icon, LevelBG;
    [SerializeField] TMP_Text Name, Level;
    [SerializeField] GameObject hintPrefab;
    level currentLevel;
    string difinition;
    GameObject spawnedHint;
    Camera cam;
    Transform parent;
    private void Start() => cam = Camera.main;
    public void SetCharacteristic(Characteristic characteristic, Transform parent)
    {
        if (characteristic == null) return;
        this.parent = parent;
        Icon.sprite = characteristic.GetIcon;
        Name.text = characteristic.GetName;
        LevelBG.color = characteristic.CurrentLevel.color;
        Level.text = characteristic.CurrentLevel.name;
        difinition = characteristic.GetDefinition;
        currentLevel = characteristic.CurrentLevel;
        progress.fillAmount = characteristic.GetLevelProgress();
    }
    private void OnMouseEnter()
    {
    }
    private void Update()
    {
        if (spawnedHint != null)
        {
            spawnedHint.transform.position = Input.mousePosition;
        }
    }
    private void OnMouseExit()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spawnedHint = Instantiate(hintPrefab, transform.position, Quaternion.identity);
        spawnedHint.transform.SetParent(parent);
        foreach (Transform t in spawnedHint.transform)
        {
            TMP_Text text = t.GetComponent<TMP_Text>();
            if (t.name == "Name") text.text = difinition;
            if (t.name == "Description") text.text = currentLevel.description;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(spawnedHint.gameObject);
        spawnedHint = null;
    }
}
