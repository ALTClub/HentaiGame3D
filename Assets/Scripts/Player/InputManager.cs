using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInput Input;

    void Awake()
    {
        Input = new PlayerInput();
        Input.Player.Enable();
    }
    private void OnDisable()
    {
        Input.Disable();
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
