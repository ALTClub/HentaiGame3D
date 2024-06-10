using System;
using System.Linq;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    [Header("Окна")]
    ///
    [SerializeField] GameObject EscMenu, Header, CharacteristicsMenu;
    bool somethingOpened = false;
    [Header("Характеристики")]
    [SerializeField] PlayerCharacteristics PlayerCharacteristics;
    private void Start()
    {
        InputManager.Input.Player.EscMenu.performed += ctx => OnEsc();
        InputManager.Input.Player.Characteristics.performed += ctx =>
        {
            ToggleWindow(CharacteristicsMenu);
            updateCharacteristics();
        };
    }
    //windows
    void CloseAll()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EscMenu.SetActive(false);
        CharacteristicsMenu.SetActive(false);
        Header.SetActive(false);
        Time.timeScale = 1;
        somethingOpened = false;
    }
    void ToggleWindow(GameObject Window)
    {
        if (EscMenu.activeSelf) return;
        CloseAll();
        Window.SetActive(!Window.activeSelf);
        Time.timeScale = (Window.activeSelf) ? 0 : 1;
        if (Window != EscMenu) Header.SetActive(Window.activeSelf);
        if (Window.activeSelf) somethingOpened = true;
    }
    void OnEsc()
    {
        if (somethingOpened || EscMenu.activeSelf) CloseAll();
        else ToggleWindow(EscMenu);
    }

    //characteristics
    void updateCharacteristics()
    {
        Cursor.lockState = CursorLockMode.None;
        foreach (Transform UIGroup in CharacteristicsMenu.transform)
            if (UIGroup.name != "Buttons") foreach (Transform UICharacteristic in UIGroup)
                    UICharacteristic.GetComponent<UICharacteristic>().SetCharacteristic(PlayerCharacteristics.GetGroup(UIGroup.name)?.GetCharacteristic(UICharacteristic.name), transform);
    }
}
