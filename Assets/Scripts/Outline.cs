using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] int _layerIndex = 4;
    [SerializeField] Renderer[] _renderers;
    private uint[] baseMasks;

    private void Awake()
    {
        baseMasks = new uint[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
            baseMasks[i] = _renderers[i].renderingLayerMask;
    }
    public bool Toggle
    {
        get => (_renderers[0].renderingLayerMask & (1u << _layerIndex)) != 0;
        set
        {
            uint bit = 1u << _layerIndex;
            for (int i = 0; i < _renderers.Length; i++)
                _renderers[i].renderingLayerMask = value ? (baseMasks[i] | bit) : (baseMasks[i] & ~bit);
        }
    }
    private void Reset()
    {
        _renderers = GetComponentsInChildren<Renderer>(true);
    }
}
