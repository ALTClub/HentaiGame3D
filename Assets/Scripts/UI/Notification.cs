using System.Collections;
using System.Collections.Generic;
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
        string message;
        Inventory.OnAddItem.AddListener((item, count, sender) =>
        {
            string action = (count < 0) ? "Отдан" : "Добавлен";
            message = $"{action} предмет!\n{item.Name} {Mathf.Abs(count)}";
            StartCoroutine(open(item.Icon, message, item.Rarity, _duration));
        });
        Characteristic.OnGetExp.AddListener((characteristic, value) =>
        {
            message = $"{characteristic.Name} \n+{value} exp"; // +25 exp
            StartCoroutine(open(characteristic.Icon, message, characteristic.Level.color, _duration));
        });
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
