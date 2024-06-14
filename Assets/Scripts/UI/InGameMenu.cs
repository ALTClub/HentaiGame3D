using System;
using System.Linq;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject _escMenu,
        _header,
        _characteristicsMenu;

    [Header("Характеристики")]
    [SerializeField]
    PlayerCharacteristics _playerCharacteristics;
    bool _somethingOpened = false;

    private void Start()
    {
        InputManager.Input.Player.EscMenu.performed += ctx => OnEsc();
        InputManager.Input.Player.Characteristics.performed += ctx =>
        {
            ToggleWindow(_characteristicsMenu);
            updateCharacteristics();
        };
    }

    //windows
    void CloseAll()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _escMenu.SetActive(false);
        _characteristicsMenu.SetActive(false);
        _header.SetActive(false);
        Time.timeScale = 1;
        _somethingOpened = false;
    }

    void ToggleWindow(GameObject Window)
    {
        if (_escMenu.activeSelf)
            return;
        CloseAll();
        Window.SetActive(!Window.activeSelf);
        Time.timeScale = (Window.activeSelf) ? 0 : 1;
        if (Window != _escMenu)
            _header.SetActive(Window.activeSelf);
        if (Window.activeSelf)
            _somethingOpened = true;
    }

    void OnEsc()
    {
        if (_somethingOpened || _escMenu.activeSelf)
            CloseAll();
        else
            ToggleWindow(_escMenu);
    }

    //characteristics
    void updateCharacteristics()
    {
        Cursor.lockState = CursorLockMode.None;
        foreach (Transform UIGroup in _characteristicsMenu.transform)
            if (UIGroup.name != "Buttons")
                foreach (Transform UICharacteristic in UIGroup)
                    UICharacteristic
                        .GetComponent<UICharacteristic>()
                        .SetCharacteristic(
                            _playerCharacteristics.GetGroup(UIGroup.name)?.GetCharacteristic(UICharacteristic.name),
                            transform
                        );
    }
}
