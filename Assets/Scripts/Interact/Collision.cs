using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Collision : MonoBehaviour
{
    [SerializeField] UnityEvent method;
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        method.Invoke();
    }
}
