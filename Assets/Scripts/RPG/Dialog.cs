using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Option
{
    [SerializeField] UnityEvent<int> _onClick;
    bool _aviable = false;
    string _name;
    public string Name => _name;
    public bool Aviable => _aviable;
    public Option(string name, bool aviable, UnityEvent<int> onClick)
    {
        _name = name;
        _aviable = aviable;
        _onClick = onClick;
    }
    public void Click()
    {
        _onClick?.Invoke(0);
    }
}

public class Phrase
{
    public UnityEvent<int> OnReaded;
    UnityEvent<int> onNext;
    TMP_Text _dialogField;
    string _name;
    string _text;
    bool _canSkip;
    public string Text => _text;
    public bool CanSkip => _canSkip;
    public Phrase(UnityEvent<int> onReaded, UnityEvent<int> onNext, string name, string text, bool canSkip)
    {
        OnReaded = onReaded;
        this.onNext = onNext;
        _name = name;
        _text = text;
        _canSkip = canSkip;
    }

    public void Skip(InputAction.CallbackContext ctx)
    {
        if (DialogWindow.reading != null)
        {
            DialogWindow.StopReading.Invoke();
            _dialogField.text = _text;
            OnReaded?.Invoke(0);
        }
        else
        {
            DialogWindow.ToggleDialog.Invoke(false);
            InputManager.Input.Dialogs.Skip.performed -= Skip;
            onNext?.Invoke(0);
        }
    }
    public void Open(TMP_Text nameField, TMP_Text dialogField)
    {
        nameField.text = _name;
        _dialogField = dialogField;
    }
}