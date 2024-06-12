using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Phrase
{
    [SerializeField][TextArea(3, 15)] private string _text;
    [SerializeField] private AudioClip _voiceActing;
    [SerializeField] static float punctuationDelay = 0.6f, lineDelay = 0.3f, charDelay = 0.1f;
    public UnityEvent OnReaded;
    TMP_Text dialogField;
    Coroutine reading;
    Delegate next;
    AudioSource source;
    public void Skip()
    {
        if (reading != null) dialogField.text = _text;
        else
        {
            Dialog.OnSkip.RemoveListener(Skip);
            next.DynamicInvoke();
        }
        reading = null;
    }
    public IEnumerator Read()
    {
        if (_voiceActing != null) source.PlayOneShot(_voiceActing);
        dialogField.text = "";
        foreach (char sym in _text)
        {
            dialogField.text += sym;
            switch (sym)
            {
                case '.':
                case ',':
                case '!':
                case '?':
                    yield return new WaitForSeconds(punctuationDelay);// время для паузы
                    break;
                case '\n':
                    yield return new WaitForSeconds(lineDelay);// время для паузы
                    break;
                default:
                    yield return new WaitForSeconds(charDelay);// время для вывода след. буквы
                    break;
            }
        }
        reading = null;
        OnReaded?.Invoke();
    }
    public void Start(MonoBehaviour mono, AudioSource source, TMP_Text dialogField, Delegate next)
    {
        this.source = source;
        this.next = next;
        this.dialogField = dialogField;
        Dialog.OnSkip.AddListener(Skip);
        reading = mono.StartCoroutine(Read());
    }
}

[Serializable]
public struct Monolog
{
    [SerializeField] private string _name;
    [SerializeField] private Phrase[] _phrases;
    [SerializeField] AudioSource source;
    int currentPhrase;
    MonoBehaviour mono;
    Delegate nextMonolog;
    TMP_Text dialogField;
    public void NextPhrase()
    {
        currentPhrase++;
        if (currentPhrase >= _phrases.Length) nextMonolog.DynamicInvoke();
        else _phrases[currentPhrase].Start(mono, source, dialogField, new Action(NextPhrase));
    }
    public void Start(MonoBehaviour mono, TMP_Text dialogField, TMP_Text nameField, Delegate nextMonolog)
    {
        this.mono = mono;
        this.dialogField = dialogField;
        currentPhrase = -1;
        this.nextMonolog = nextMonolog;
        nameField.text = _name;
        NextPhrase();
    }
}

[Serializable]
public class Dialog
{
    [SerializeField] private Monolog[] _dialog;
    public static UnityEvent OnSkip = new();
    MonoBehaviour mono;
    TMP_Text dialogField, nameField;
    int currentMonolog;
    Delegate stop;
    public void NextMonolog()
    {
        if (currentMonolog >= _dialog.Length) stop.DynamicInvoke();
        else _dialog[currentMonolog].Start(mono, dialogField, nameField, new Action(NextMonolog));
        currentMonolog++;
    }
    public void Start(MonoBehaviour mono, TMP_Text dialogField, TMP_Text nameField, Delegate stop)
    {
        currentMonolog = 0;
        this.mono = mono;
        this.nameField = nameField;
        this.dialogField = dialogField;
        this.stop = stop;
        NextMonolog();
    }
}