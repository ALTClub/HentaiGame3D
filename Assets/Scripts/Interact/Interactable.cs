using EPOOutline;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string[] _prompt;
    [SerializeField] protected Outlinable _outline;
    public abstract void Interact(GameObject sender);
    public virtual void OnSelect(bool enabled)
    {
        if (_outline != null)
            _outline.enabled = enabled;
    }
    public virtual string GetPrompt => _prompt[0];
    public virtual bool IsAviable => true;
}
