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
        public Item Item;
        public int Count = 0;
        public Slot(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }
    public static UnityEvent<Item, int, object> OnAddItem = new();
    List<Slot> _inventory = new();
    private void Start()
    {
        load();
        SaveManager.OnSave.AddListener(save);
    }
    public void UpdateCountBy(Item item, int count = 1)
    {
        Slot slot = _inventory.Find(slot => slot.Item == item);

        if (slot != default && slot.Count + count >= 0)
        {
            slot.Count += count;
            if (slot.Count == 0) _inventory.Remove(slot);
        }
        else if (count > 0) _inventory.Add(new Slot(item, count));
        else
        {
            Debug.Log("not enaugh");
            return;
        }

        OnAddItem.Invoke(item, count, this);
    }
    public int GetItemCount(Item item)
    {
        var slot = _inventory.Find(slot => slot.Item == item);
        return slot == default ? 0 : slot.Count;
    }
    void save()
    {
        Serialization<string, string> data = new(_inventory.ToDictionary(slot => slot.Item.name, slot => slot.Count.ToString()));
        SaveManager.UpdateSaveData(this, JsonUtility.ToJson(data), !Global);
    }
    void load()
    {
        string json = SaveManager.GetSaveData(this, !Global);
        if (json == default) return;
        var dict = JsonUtility.FromJson<Serialization<string, string>>(json).ToDictionary();
        foreach (var slot in dict)
            _inventory.Add(new Slot(Resources.Load<Item>($"Items/{slot.Key}"), int.Parse(slot.Value)));
    }
}