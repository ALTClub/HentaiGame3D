using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Input playerInputComponent;
    public void Close()
    {
        playerInputComponent.EnableControls();
    }

    public void Open()
    {
        playerInputComponent.DisableControls();
    }
}
