using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractAble : MonoBehaviour
{
    [SerializeField] private string _name;
    public void Interact()
    {

    }
    public string GetName { get { return _name; } }
}
