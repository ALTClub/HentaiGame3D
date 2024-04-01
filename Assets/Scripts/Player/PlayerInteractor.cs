using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] string interactableTag;
    private List<InteractAble> interactAbles = new List<InteractAble>();
    public static UnityEvent<List<InteractAble>> OnInteractUpdate = new UnityEvent<List<InteractAble>>();
    public static UnityEvent<int> OnSellectInteraction = new UnityEvent<int>();
    int currentInteract = 0;
    private void Start()
    {
        InputManager.Input.Player.Interact.performed += ctx => interact();
        InputManager.Input.Player.Sellect.performed += ctx => sellect((int)ctx.ReadValue<float>());
        OnInteractUpdate.AddListener(ctx => currentInteract = 0);
    }
    void sellect(int axis)
    {
        axis /= Math.Abs(axis);
        if (currentInteract + axis >= interactAbles.Count || currentInteract + axis < 0) return;
        currentInteract += axis;
        OnSellectInteraction?.Invoke(currentInteract);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(interactableTag))
        {
            InteractAble interact = other.GetComponent<InteractAble>();
            interactAbles.Add(interact);
            OnInteractUpdate.Invoke(interactAbles);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(interactableTag))
        {
            InteractAble interact = other.GetComponent<InteractAble>();
            interactAbles.Remove(interact);
            OnInteractUpdate.Invoke(interactAbles);
        }
    }
    void interact()
    {
        if (interactAbles.Count > currentInteract)
        {
            interactAbles[currentInteract]?.Interact();
            OnInteractUpdate.Invoke(interactAbles);
        }
    }
}
