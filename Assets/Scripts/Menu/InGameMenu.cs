using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Player player = null;
    public void Close()
    {
        player?.EnableControls();
    }

    public void Open()
    {
        player?.DisableControls();
    }
}
