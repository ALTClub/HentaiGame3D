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
    public static UnityEvent<Item> OnAddItem = new();
    List<Slot> _inventory = new();
    public void AddItem(Item item, int count = 1)
    {
        foreach (Slot slot in _inventory)
            if (slot._item == item)
            {
                slot._count += count;
                return;
            }
        _inventory.Add(new Slot(item, count));
        OnAddItem.Invoke(item);
    }
    Item _selected;
    public void SelectItem(Item item) => _selected = item;
    public void AddSelectedItem(int count) => AddItem(_selected, count);
}