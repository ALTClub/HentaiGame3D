using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static PlayerInput Input;

    void Awake()
    {
        Input = new PlayerInput();
        EnableControls();
    }

    private void OnDisable() => DisableControls();
    public static void DisableControls() => Input.Disable();
    public static void EnableControls() => Input.Player.Enable();
}
