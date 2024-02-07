using UnityEngine;

public class SpinningXObject : MonoBehaviour
{
    [SerializeField]
    private new GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        camera.transform.Rotate(1f, 0f, 0f, Space.Self);
    }
}
