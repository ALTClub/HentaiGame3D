using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string prompt;
    public abstract void Interact();
    public virtual void OnSelect() { }
    public string GetPrompt { get { return prompt; } }
}
