using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInput Input;

    void Awake()
    {
        Input = new PlayerInput();
        this.EnableControls();
    }

    private void OnDisable()
    {
        this.DisableControls();
    }

    public void DisableControls()
    {
        Input.Disable();
    }

    public void EnableControls()
    {
        Input.Enable();
    }
}
