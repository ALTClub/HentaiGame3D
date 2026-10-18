using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string[] _prompt;
    [SerializeField] protected Outline _outline;
    public abstract void Interact(GameObject sender);
    public virtual void OnSelect(bool enabled)
    {
        if (_outline != null)
            _outline.Toggle = enabled;
    }
    public virtual string GetPrompt => _prompt[0];
    public virtual bool IsAviable => true;
}
