using EPOOutline;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string _prompt;
    [SerializeField] protected Outlinable _outline;
    public abstract void Interact();
    public virtual void OnSelect(bool enabled)
    {
        if (_outline != null)
            _outline.enabled = enabled;
    }
    public string GetPrompt { get { return _prompt; } }
}
