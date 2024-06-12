using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNPC : Interactable
{
    [SerializeField] Dialog dialog;
    public override void Interact()
    {
        DialogWindow.PlayDialog.Invoke(dialog);
    }
}
