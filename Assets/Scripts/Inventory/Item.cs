using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Sprite _icon;
    [SerializeField] GameObject _prefab;
    [SerializeField] Color _rarity;
    public string Name => _name;
    public Sprite Icon => _icon;
    public Color Rarity => _rarity;
}
