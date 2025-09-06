using System.Collections;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] Item _item;
    [SerializeField] int _count = 1;
    SaveData<bool> isCollected;
    private void Awake()
    {
        isCollected = new(this, false, false);
        if (isCollected.Value) StartCoroutine(collect());
    }
    public override void Interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().UpdateCountBy(_item, _count);
        StartCoroutine(collect());
    }
    IEnumerator collect()
    {
        transform.Translate(0, -1000, 0);
        yield return new WaitForSeconds(1);
        isCollected.Value = true;
        Destroy(gameObject);
    }
}
