using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Rotate(float rotationSpeed, Transform player)
    {
        Quaternion rotation = transform.rotation;
        rotation.x = player.rotation.x;
        rotation.z = player.rotation.z;
        player.rotation = Quaternion.Lerp(player.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
