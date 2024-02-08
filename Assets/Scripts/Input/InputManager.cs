using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager: MonoBehaviour
{
    public static PlayerInput Input = new PlayerInput();
    public static event Action<InputActionMap> ActionMapChange;
    //private static InputActionMap lastActionMap = Input.Player;
    private void Awake()
    {
        Input = new PlayerInput();
        DontDestroyOnLoad(gameObject);
    }

    public static void ExitMenu()
    {
        //ToggleActionMap(lastActionMap);
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        Input.Disable();
        ActionMapChange?.Invoke(actionMap);
        if (actionMap != null)
        {
            actionMap.Enable();
            //if(actionMap != (InputActionMap)Input.Menu) lastActionMap = actionMap;
        }
        //Input.Menu.Toggle.Enable();
    }
}
