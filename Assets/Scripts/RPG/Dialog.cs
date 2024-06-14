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
    [SerializeField] bool isAndConditions;
    [SerializeField] Condition.Characteristic[] characteristicConditions;
    [SerializeField] UnityEvent OnClick;
    [SerializeField] Monolog continuation;

    public string Name => _name;

    TMP_Text _nameField;
    TMP_Text _dialogField;
    public bool CheckConditions()
    {
        Condition[] conditions = characteristicConditions;
        if (conditions.Length == 0) return true;
        if (isAndConditions)
        {
            foreach (Condition condition in conditions)
                if (!condition.check()) return false;
            return true;
        }
        else
        {
            foreach (Condition condition in conditions)
                if (condition.check()) return true;
            return false;
        }
    }
    public void Click()
    {
        OnClick?.Invoke();
        continuation.Start(_dialogField, _nameField);
    }

    public void Start(TMP_Text nameField, TMP_Text dialogField)
    {
        _nameField = nameField;
        _dialogField = dialogField;
    }
}


[Serializable]
public struct Phrase
{
    [SerializeField][TextArea(3, 15)] private string _text;
    [SerializeField] private AudioClip _voiceActing;
    public UnityEvent OnReaded;
    TMP_Text dialogField;
    Delegate next;
    public void Skip()
    {
        if (DialogWindow.reading != null)
        {
            dialogField.text = _text;
            DialogWindow.Read.Invoke("", OnReaded);
        }
        else
        {
            Dialog.OnSkip.RemoveListener(Skip);
            next.DynamicInvoke();
        }
    }
    public void Start(AudioSource source, TMP_Text dialogField, Delegate next)
    {
        this.next = next;
        this.dialogField = dialogField;
        Dialog.OnSkip.AddListener(Skip);
        if (_voiceActing != null) source.PlayOneShot(_voiceActing);
        DialogWindow.Read.Invoke(_text, OnReaded);
    }
}

[Serializable]
public struct Monolog
{
    [SerializeField] private string _name;
    [SerializeField] private Phrase[] _phrases;
    [SerializeField] AudioSource source;
    [SerializeField] List<Option> _options;
    int currentPhrase;
    TMP_Text dialogField;
    public void NextPhrase()
    {
        if (currentPhrase + 1 == _phrases.Length) return;
        currentPhrase++;
        _phrases[currentPhrase].Start(source, dialogField, new Action(NextPhrase));
        if (currentPhrase + 1 == _phrases.Length)
        {
            if (_options.Count > 0) DialogWindow.SpawnOptions.Invoke(_options);
            else DialogWindow.Close.Invoke();
        }
    }
    public void Start(TMP_Text dialogField, TMP_Text nameField)
    {
        currentPhrase = -1;
        this.dialogField = dialogField;
        nameField.text = _name;
        NextPhrase();
    }
}


[Serializable]
public class Dialog
{
    [SerializeField] private List<Option> _options;
    public static UnityEvent OnSkip = new();



    public void Start()
    {
        DialogWindow.SpawnOptions.Invoke(_options);
    }








    /*
    public void NextMonolog()
    {
        if (currentMonolog >= _monolog.Length) stop.DynamicInvoke();
        else _monolog[currentMonolog].Start(mono, dialogField, nameField, new Action(NextMonolog));
        currentMonolog++;
    }*/
}