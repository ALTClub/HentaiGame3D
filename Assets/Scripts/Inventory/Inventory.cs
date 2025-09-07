using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] bool _isGlobal;
    // разделить на группы
    public class Slot
    {
        [JsonIgnore] public Item Item;
        public int Count = 0;
        public string ItemName { get => Item.name; set { Item = Resources.Load<Item>($"Items/{value}"); } }
        public Slot(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }
    public static UnityEvent<Item, int, object> OnAddItem = new();
    SaveData<List<Slot>> inventory;
    private void Awake()
    {
        inventory = new(this, _isGlobal, new());
    }
    public void UpdateCountBy(Item item, int count = 1)
    {
        Slot slot = inventory.Value.Find(slot => slot.Item == item);

        if (slot != default && slot.Count + count >= 0)
        {
            slot.Count += count;
            if (slot.Count == 0) inventory.Value.Remove(slot);
        }
        else if (count > 0) inventory.Value.Add(new Slot(item, count));
        else
        {
            Debug.Log("not enaugh");
            return;
        }

        OnAddItem.Invoke(item, count, this);
    }
    public int GetItemCount(Item item)
    {
        var slot = inventory.Value.Find(slot => slot.Item == item);
        return slot == default ? 0 : slot.Count;
    }
}