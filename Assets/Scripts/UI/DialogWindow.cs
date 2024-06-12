using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _text;
    [SerializeField][Range(0, 0.5f)] private float charDelay = 0.05f;
    [SerializeField][Range(0, 1)] private float punctuationDelay;
    [SerializeField][Range(0, 1)] private float lineDelay;
    public static UnityEvent<Dialog> PlayDialog = new();
    private void OnEnable()
    {
        PlayDialog.AddListener(Initialize);
    }
    private void OnDisable()
    {
        InputManager.DisableControls();
        InputManager.EnableControls();
        window.SetActive(false);
    }
    public void Initialize(Dialog dialog)
    {
        InputManager.DisableControls();
        InputManager.Input.Dialogs.Enable();
        InputManager.Input.Dialogs.Skip.performed += ctx => Dialog.OnSkip.Invoke();
        dialog.Start(this, _text, _name, new Action(close));
        window.SetActive(true);
    }
    private void close()
    {
        InputManager.DisableControls();
        InputManager.EnableControls();
        window.SetActive(false);
    }
}
