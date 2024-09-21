using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] string interactableTag;

    private List<Interactable> interactables = new List<Interactable>();
    public static UnityEvent<List<Interactable>> OnInteractUpdate = new UnityEvent<List<Interactable>>();
    public static UnityEvent<int> OnSellectInteraction = new UnityEvent<int>();

    int currentInteract = 0;

    void Start()
    {
        InputManager.Input.Player.Interact.performed += ctx => interact();
        InputManager.Input.Player.Sellect.performed += ctx => select((int)ctx.ReadValue<float>());
        OnInteractUpdate.AddListener(ctx => currentInteract = 0);
        OnInteractUpdate.AddListener(ctx => updateInteractables());
    }

    void select(int axis)
    {
        axis /= Math.Abs(axis);
        if (currentInteract + axis >= interactables.Count || currentInteract + axis < 0) return;
        currentInteract += axis;
        OnSellectInteraction?.Invoke(currentInteract);
        updateInteractables();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable interact))
        {
            interactables.Add(interact);
            OnInteractUpdate.Invoke(interactables);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable interact))
        {
            interact.OnSelect(false);
            interactables.Remove(interact);
            OnInteractUpdate.Invoke(interactables);
        }
    }

    void interact()
    {
        if (interactables.Count > currentInteract)
        {
            interactables[currentInteract]?.Interact();
            OnInteractUpdate.Invoke(interactables);
        }
    }
    void updateInteractables()
    {
        if (interactables.Count == 0) return;
        foreach (Interactable interactable in interactables)
            interactable.OnSelect(false);
        interactables[currentInteract].OnSelect(true);
    }
}
