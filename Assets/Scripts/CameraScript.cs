using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public Vector3 Rotate(Vector3 direction, Transform player)
    {
        // --- Character rotation --- 
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Relate the front with the Z direction (depth) and right with X (lateral movement)
        forward = forward * direction.z;
        right = right * direction.x;

        if (direction.x != 0 || direction.z != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            player.rotation = Quaternion.Slerp(player.rotation, rotation, 0.15f);
        }

        // --- End rotation ---
        Vector3 verticalDirection = Vector3.up * direction.y;
        Vector3 horizontalDirection = forward + right;
        return verticalDirection + horizontalDirection;
    }
}
