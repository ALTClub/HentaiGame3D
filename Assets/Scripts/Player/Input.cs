using UnityEngine;

public class Input : MonoBehaviour
{
    public PlayerInput map;

    void Start()
    {
        map = new PlayerInput();
    }

    public void DisableControls()
    {
        map.Disable();
    }

    public void EnableControls()
    {
        map.Enable();
    }
}
