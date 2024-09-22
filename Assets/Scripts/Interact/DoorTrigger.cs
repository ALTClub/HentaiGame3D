using System;
using UnityEngine;

[Serializable]
public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Door door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            door.IsPlayerCollision = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            door.IsPlayerCollision = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            door.IsPlayerCollision = false;
    }
}
