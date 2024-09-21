using EPOOutline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string prompt;
    [SerializeField] protected Outlinable outline;
    public abstract void Interact();
    public virtual void OnSelect(bool enabled)
    {
        if (outline != null)
            outline.enabled = enabled;
    }
    public string GetPrompt { get { return prompt; } }
}
