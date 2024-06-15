using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Option
{
    [SerializeField] string _name;
    [SerializeField] bool _isAndConditions;
    [SerializeField] Condition.Characteristic[] _characteristicConditions;
    [SerializeField] UnityEvent _onClick;
    [SerializeField] Monolog _continuation;

    TMP_Text _nameField;
    TMP_Text _dialogField;
    public string Name => _name;

    public bool CheckConditions()
    {
        Condition[] conditions = _characteristicConditions;
        if (conditions.Length == 0) return true;
        if (_isAndConditions)
        {
            foreach (Condition condition in conditions)
                if (!condition.Сheck()) return false;
            return true;
        }
        else
        {
            foreach (Condition condition in conditions)
                if (condition.Сheck()) return true;
            return false;
        }
    }
    public void Click()
    {
        _onClick?.Invoke();
        _continuation.Open(_dialogField, _nameField);
    }

    public void Open(TMP_Text nameField, TMP_Text dialogField)
    {
        _nameField = nameField;
        _dialogField = dialogField;
    }
}


[Serializable]
public struct Phrase
{
    [SerializeField][TextArea(3, 15)] string _text;
    [SerializeField] AudioClip _voiceActing;
    [SerializeField] UnityEvent _onReaded;

    TMP_Text _dialogField;
    Delegate _next;

    public void Skip()
    {
        if (DialogWindow.reading != null)
        {
            _dialogField.text = _text;
            DialogWindow.Read.Invoke("", _onReaded);
        }
        else
        {
            Dialog.OnSkip.RemoveListener(Skip);
            _next.DynamicInvoke();
        }
    }
    public void Open(AudioSource source, TMP_Text dialogField, Delegate next)
    {
        _next = next;
        _dialogField = dialogField;
        if (_voiceActing != null) source.PlayOneShot(_voiceActing);
        Dialog.OnSkip.AddListener(Skip);
        DialogWindow.Read.Invoke(_text, _onReaded);
    }
}

[Serializable]
public struct Monolog
{
    [SerializeField] string _name;
    [SerializeField] Phrase[] _phrases;
    [SerializeField] AudioSource _source;
    [SerializeField] List<Option> _options;

    TMP_Text _dialogField;
    int _currentPhrase;
    public void NextPhrase()
    {
        if (_currentPhrase + 1 == _phrases.Length) return;
        _currentPhrase++;
        _phrases[_currentPhrase].Open(_source, _dialogField, new Action(NextPhrase));
        if (_currentPhrase + 1 == _phrases.Length)
        {
            if (_options.Count > 0) DialogWindow.SpawnOptions.Invoke(_options);
            else DialogWindow.Stop.Invoke();
        }
    }
    public void Open(TMP_Text dialogField, TMP_Text nameField)
    {
        _currentPhrase = -1;
        _dialogField = dialogField;
        nameField.text = _name;
        NextPhrase();
    }
}


[Serializable]
public class Dialog
{
    [SerializeField] private List<Option> _options;
    public static UnityEvent OnSkip = new();

    public void Open()
    {
        DialogWindow.SpawnOptions.Invoke(_options);
    }
}