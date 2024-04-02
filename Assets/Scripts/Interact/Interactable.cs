using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string prompt;
    private void Awake()
    {
        gameObject.tag = "Interactable";
    }
    public abstract void Interact();
    public string GetPrompt { get { return prompt; } }
}
