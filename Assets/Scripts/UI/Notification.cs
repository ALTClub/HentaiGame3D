using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _message;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        string message;
        Inventory.OnAddItem.AddListener(itemNotification);
        Characteristic.OnNewLevel.AddListener(characteristic =>
        {
            message = "Новый уровень!\n"
            + characteristic.name + '\n'
            + characteristic.Level.name;
            StartCoroutine(open(characteristic.Icon, message, characteristic.Level.color, 1.5f));
        });
    }
    void itemNotification(Item item, object sender)
    {
        Debug.Log(sender);
        StartCoroutine(open(item.Icon, $"Получен предмет!\n{item.name}", item.Rarity, 1f));
    }
    IEnumerator open(Sprite icon, string message, Color color, float duration)
    {
        _icon.sprite = icon;
        _message.text = message;
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("Close");
    }
}
