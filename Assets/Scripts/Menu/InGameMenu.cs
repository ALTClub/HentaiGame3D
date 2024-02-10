using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private InputManager playerInputComponent;
    public void Close()
    {
        playerInputComponent.EnableControls();
    }

    public void Open()
    {
        playerInputComponent.DisableControls();
    }
}
