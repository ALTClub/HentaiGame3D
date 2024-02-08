using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput input;

    void Start()
    {
        input = new PlayerInput();
    }

    public void DisableControls()
    {
        input.Disable();
    }
    public void EnableControls()
    {
        input.Enable();
    }
}
