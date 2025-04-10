using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    // разделить на группы
    public class Slot
    {
        public Item _item;
        public int _count = 0;
        public Slot(Item item, int count)
        {
            _item = item;
            _count = count;
        }
    }
    public static UnityEvent<Item, object> OnAddItem = new();
    List<Slot> _inventory = new();
    public void UpdateCountBy(Item item, int count = 1)
    {
        Slot slot = _inventory.Find(slot => slot._item == item);
        if (slot == default) _inventory.Add(new Slot(item, count));
        else
        {
            if (slot._count + count < 0)
            {
                Debug.Log("not enaugh");
                return;
            }
            slot._count += count;
        }

        string action = (count < 0) ? "Удалён" : "Добавлен";
        Debug.Log($"{action} предмет: {item} {Mathf.Abs(count)}");
        OnAddItem.Invoke(item, this);
    }
    public int GetItemCount(Item item)
    {
        var slot = _inventory.Find(slot => slot._item == item);
        return slot == default ? 0 : slot._count;
    }
}