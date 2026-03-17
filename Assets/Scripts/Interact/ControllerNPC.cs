using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Events;

public class ControllerNPC : Interactable
{
    public override void Interact(GameObject sender)
    {
        CustomEvent.Trigger(gameObject, "Interact");
        //DialogWindow.PlayDialog.Invoke(dialog);
    }
    public void SaveTrigger() => SaveManager.OnSave.Invoke();
}
