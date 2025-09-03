using System.Collections;
using UnityEngine;

public class Collectable : Interactable
{
    [SerializeField] Item _item;
    [SerializeField] int _count = 1;
    public override void Interact()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().UpdateCountBy(_item, _count);
        save();
        StartCoroutine(collect());
    }
    IEnumerator collect()
    {
        transform.Translate(0, -1000, 0);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    protected override void onLoad()
    {
        if (SaveManager.GetSaveData<bool>(this) != default)
            StartCoroutine(collect());
    }
    void save() => SaveManager.UpdateSaveData(this, true);
}
