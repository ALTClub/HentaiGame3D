using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInput Input;

    void Awake()
    {
        Input = new PlayerInput();
        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }

    public void DisableControls() => Input.Disable();

    public void EnableControls() => Input.Enable();
}
