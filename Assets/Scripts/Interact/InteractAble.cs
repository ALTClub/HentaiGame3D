using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAble : MonoBehaviour
{
    [SerializeField] protected string _name;
    public abstract void Interact();
    public string GetName { get { return _name; } }
}
