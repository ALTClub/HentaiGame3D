using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float rotationSpeed;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }
    private void FixedUpdate()
    {
        //if (!InputManager.Input.Player.Movement.IsPressed()) return;
        //player.rotation = Quaternion.LookRotation(transform.forward, player.up);
        Quaternion rotation = transform.rotation;
        rotation.x = player.rotation.x; rotation.z = player.rotation.z;
        //player.rotation = Quaternion.Lerp(player.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
