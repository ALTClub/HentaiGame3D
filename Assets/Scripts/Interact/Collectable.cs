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
        _playerInventory.ChangeItem(_item, _count);
        Destroy(gameObject);
    }
}
