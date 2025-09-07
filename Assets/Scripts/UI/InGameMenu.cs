using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject _escMenu, _header, _characteristicsMenu, _loadingWindow;
    [SerializeField] LoadMenu _loadMenu;
    [Header("Характеристики")]
    [SerializeField] MyCharacteristics _playerCharacteristics;
    bool _somethingOpened = false;
    private void Start()
    {
        InputManager.Input.Player.EscMenu.performed += onEsc;
        InputManager.Input.Player.Characteristics.performed += toggleCharacteristics;
    }
    private void OnDisable()
    {
        InputManager.Input.Player.EscMenu.performed -= onEsc;
        InputManager.Input.Player.Characteristics.performed -= toggleCharacteristics;
    }
    public void ResetButton() => SaveManager.ClearData();
    //windows
    public void CloseAll()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        _escMenu.SetActive(false);
        _characteristicsMenu.SetActive(false);
        _header.SetActive(false);
        _loadingWindow.SetActive(false);

        _somethingOpened = false;
    }
    void toggleWindow(GameObject Window)
    {
        CloseAll();
        Cursor.lockState = CursorLockMode.None;

        Window.SetActive(!Window.activeSelf);
        Time.timeScale = (Window.activeSelf) ? 0 : 1;
        if (Window != _escMenu) _header.SetActive(Window.activeSelf);
        if (Window.activeSelf) _somethingOpened = true;
    }
    void onEsc(InputAction.CallbackContext ctx)
    {
        if (_somethingOpened) CloseAll();
        else
        {
            _loadMenu.UpdateButton();
            toggleWindow(_escMenu);
        }
    }

    //characteristics
    void toggleCharacteristics(InputAction.CallbackContext ctx)
    {
        if (_escMenu.activeSelf) return;
        toggleWindow(_characteristicsMenu);
    }
    public void MainMenu() => SceneManager.LoadSceneAsync(0);
}
