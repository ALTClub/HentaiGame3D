using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _message;
    [SerializeField] float _duration = 1f;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        Inventory.OnAddItem.AddListener(handleItemAdded);
        ExpCharacteristic.OnGetExp.AddListener(handleExpGained);
    }
    private void handleItemAdded(Item item, int count, object sender)
    {
        string action = (count < 0) ? "Отдан" : "Добавлен";
        string message = $"{action} предмет!\n{item.Name} {Mathf.Abs(count)}";
        StartCoroutine(open(item.Icon, message, item.Rarity, _duration));
    }
    private void handleExpGained(ExpCharacteristic expChar, int value, bool isNewLevel)
    {
        string message = $"{expChar.Characteristic.Name} \n" +
            (isNewLevel ? $"New level: {expChar.Level.name}\n" : "" + $"+{value} exp");
        StartCoroutine(open(expChar.Characteristic.Icon, message, expChar.Level.color, _duration));
    }
    IEnumerator open(Sprite icon, string message, Color color, float duration)
    {
        Debug.Log(message);
        _icon.sprite = icon;
        _message.text = message;
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("Close");
    }
}
