using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject _escMenu, _header, _characteristicsMenu;
    [Header("Характеристики")]
    [SerializeField] PlayerCharacteristics _playerCharacteristics;
    bool _somethingOpened = false;
    private void Start()
    {
        InputManager.Input.Player.EscMenu.performed += OnEsc;
        InputManager.Input.Player.Characteristics.performed += toggleCharacteristics;
    }
    private void OnDisable()
    {
        InputManager.Input.Player.EscMenu.performed -= OnEsc;
        InputManager.Input.Player.Characteristics.performed -= toggleCharacteristics;
    }
    public void ResetButton() => SaveManager.ClearData();
    //windows
    void CloseAll()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        _escMenu.SetActive(false);
        _characteristicsMenu.SetActive(false);
        _header.SetActive(false);

        _somethingOpened = false;
    }
    void ToggleWindow(GameObject Window)
    {
        CloseAll();
        Cursor.lockState = CursorLockMode.None;

        Window.SetActive(!Window.activeSelf);
        Time.timeScale = (Window.activeSelf) ? 0 : 1;
        if (Window != _escMenu) _header.SetActive(Window.activeSelf);
        if (Window.activeSelf) _somethingOpened = true;
    }
    void OnEsc(InputAction.CallbackContext ctx)
    {
        if (_somethingOpened) CloseAll();
        else ToggleWindow(_escMenu);
    }

    //characteristics
    void toggleCharacteristics(InputAction.CallbackContext ctx)
    {
        if (_escMenu.activeSelf) return;
        ToggleWindow(_characteristicsMenu);
    }
}
