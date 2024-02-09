using UnityEngine;

public class SpinningXObject : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.Rotate(1f, 0f, 0f, Space.Self);
    }
}
