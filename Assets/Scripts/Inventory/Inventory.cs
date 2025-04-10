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
    public void ChangeItem(Item item, int count = 1)
    {
        foreach (Slot slot in _inventory)
            if (slot._item == item)
            {
                if (slot._count + count < 0)
                {
                    Debug.Log("not enaugh");
                    return;
                }
                slot._count += count;
                break;
            }
        _inventory.Add(new Slot(item, count));
        string action = (count < 0) ? "Удалён" : "Добавлен";
        Debug.Log($"{action} предмет: {item} {Mathf.Abs(count)}");
        OnAddItem.Invoke(item, this);
    }
    public int GetItemCount(Item item)
    {
        foreach (Slot slot in _inventory)
            if (slot._item == item)
                return slot._count;
        return 0;
    }
}