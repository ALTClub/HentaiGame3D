using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] bool Global;
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
    private void Start()
    {
        load();
        SaveManager.OnSave.AddListener(save);
    }
    public void UpdateCountBy(Item item, int count = 1)
    {
        Slot slot = _inventory.Find(slot => slot._item == item);

        if (slot != default && slot._count + count >= 0)
        {
            slot._count += count;
            if (slot._count == 0) _inventory.Remove(slot);
        }
        else if (count > 0) _inventory.Add(new Slot(item, count));
        else
        {
            Debug.Log("not enaugh");
            return;
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
    void save()
    {
        string data = string.Join('\n', _inventory.Select(slot => $"{slot._item.name}={slot._count}"));
        SaveManager.Save(this, data, !Global);
    }
    void load()
    {
        string data = SaveManager.GetData(this, !Global);
        if (data == default) return;
        var slots = data.Split('\n').Select(slot => slot.Split('='));
        foreach (var slot in slots)
            _inventory.Add(new Slot(Resources.Load<Item>($"Items/{slot[0]}"), int.Parse(slot[1])));
    }
}