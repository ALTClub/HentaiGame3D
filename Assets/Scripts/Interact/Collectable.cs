using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] Item _item;
    [SerializeField] int _count = 1;
    Inventory _playerInventory;
    private void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Inventory>();
    }
    public override void Interact()
    {
        _playerInventory.UpdateCountBy(_item, _count);
        save();
        Destroy(gameObject);
    }
    protected override void onLoad()
    {
        var data = SaveManager.GetSaveData(this);
        if (data != default && bool.Parse(data)) Destroy(gameObject);
    }
    void save()
    {
        SaveManager.UpdateSaveData(this, true.ToString());
    }
}
