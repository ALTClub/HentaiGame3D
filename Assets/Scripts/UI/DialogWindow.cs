
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] GameObject _window;
    [SerializeField] TMP_Text _name, _text;
    [SerializeField] Unity.Cinemachine.CinemachineFreeLook _cam;

    [Header("Задержки")]
    [SerializeField][Range(0, 0.5f)] float _charDelay = 0.05f;
    [SerializeField][Range(0, 1)] float _punctuationDelay;
    [SerializeField][Range(0, 1)] float _lineDelay;

    [Header("Варианты")]
    [SerializeField] GameObject _optionPrefab;
    [SerializeField] Transform _optionPose;
    [SerializeField] float _spacing;
    List<GameObject> _optionList = new();

    public static UnityEvent<Phrase> Read = new();
    public static UnityEvent StopReading = new();
    public static UnityEvent<bool> ToggleDialog = new();
    private void OnEnable()
    {
        Read.AddListener(reader);
        StopReading.AddListener(stop);
        ToggleDialog.AddListener(toggle);
    }
    void OnDisable()
    {
        InputManager.DisableControls();
        InputManager.EnableControls();
        _window.SetActive(false);
    }
    // base
    void toggle(bool enabled)
    {
        _text.text = "";
        _window.SetActive(enabled);

        InputManager.DisableControls();
        if (enabled) InputManager.Input.Dialogs.Enable();
        else InputManager.EnableControls();
    }
    //reading
    public static Coroutine reading { get; private set; }
    void stop()
    {
        if (reading != null)
        {
            StopCoroutine(reading);
            reading = null;
        }
    }
    void reader(Phrase phrase)
    {
        toggle(true);

        phrase.Open(_name, _text);
        InputManager.Input.Dialogs.Skip.performed += phrase.Skip;
        stop();
        reading = StartCoroutine(spelling(phrase.Text, phrase.OnReaded));
    }
    IEnumerator spelling(string text, UnityEvent<int> OnReaded)
    {
        _text.text = "";
        foreach (char sym in text)
        {
            _text.text += sym;
            switch (sym)
            {
                case '.':
                case ',':
                case '!':
                case '?':
                    yield return new WaitForSeconds(_punctuationDelay);// время для паузы
                    break;
                case '\n':
                    yield return new WaitForSeconds(_lineDelay);// время для паузы
                    break;
                default:
                    yield return new WaitForSeconds(_charDelay);// время для вывода след. буквы
                    break;
            }
        }
        reading = null;
        OnReaded.Invoke(0);
    }
    // options
    void closeOptions()
    {
        foreach (GameObject option in _optionList) Destroy(option.gameObject);
        _optionList.Clear();
        InputManager.EnableControls();
        Cursor.lockState = CursorLockMode.Locked;
        _cam.enabled = true;
    }
    public void SpawnOptions(Option[] options)
    {
        _cam.enabled = false;
        InputManager.DisableControls();
        Cursor.lockState = CursorLockMode.None;
        foreach (var option in options)
        {
            GameObject Option = Instantiate(_optionPrefab, _optionPose);
            Option.GetComponentInChildren<TMP_Text>().text = option.Name;

            Button button = Option.GetComponent<Button>();
            button.interactable = option.Aviable;
            button.name = option.Name;

            button.onClick.AddListener(closeOptions);
            button.onClick.AddListener(option.Click);
            _optionList.Add(Option);
        }
    }
}
