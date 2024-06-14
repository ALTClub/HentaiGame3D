using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] GameObject _window;
    [SerializeField] TMP_Text _name, _text;
    [SerializeField] CinemachineFreeLook _cam;

    [Header("Задержки")]
    [SerializeField][Range(0, 0.5f)] float _charDelay = 0.05f;
    [SerializeField][Range(0, 1)] float _punctuationDelay;
    [SerializeField][Range(0, 1)] float _lineDelay;

    [Header("Варианты")]
    [SerializeField] GameObject _optionPrefab;
    [SerializeField] Transform _optionPose;
    [SerializeField] float _spacing;
    List<GameObject> _optionList = new();

    public static UnityEvent<Dialog> PlayDialog = new();
    public static UnityEvent<String, UnityEvent> Read = new();
    public static UnityEvent<List<Option>> SpawnOptions = new();
    public static UnityEvent Stop = new();

    void OnEnable()
    {
        PlayDialog.AddListener(Play);
        Read.AddListener(Reader);
        SpawnOptions.AddListener(spawnOptions);
        Stop.AddListener(Close);
    }
    void OnDisable()
    {
        InputManager.DisableControls();
        InputManager.EnableControls();
        _window.SetActive(false);
    }
    // base
    public void Play(Dialog dialog)
    {
        _cam.enabled = false;

        InputManager.DisableControls();
        InputManager.Input.Dialogs.Enable();
        InputManager.Input.Dialogs.Skip.performed += ctx => Dialog.OnSkip.Invoke();

        Cursor.lockState = CursorLockMode.None;
        dialog.Open();
        _window.SetActive(true);
    }
    public void Close()
    {
        InputManager.DisableControls();
        InputManager.EnableControls();
        Cursor.lockState = CursorLockMode.Locked;
        _window.SetActive(false);
        _cam.enabled = true;
    }
    public static Coroutine reading { get; private set; }
    void Reader(string text, UnityEvent OnReaded)
    {
        if (reading != null) StopCoroutine(reading);
        if (text != "") reading = StartCoroutine(reader(text, OnReaded));
        else OnReaded.Invoke();
    }
    IEnumerator reader(string text, UnityEvent OnReaded)
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
        OnReaded.Invoke();
    }
    // options
    void closeOptions()
    {
        foreach (GameObject option in _optionList) Destroy(option.gameObject);
        _optionList.Clear();
    }
    void spawnOptions(List<Option> options)
    {
        foreach (var option in options)
        {
            option.Open(_name, _text);
            GameObject Option = Instantiate(_optionPrefab, transform.position, Quaternion.identity);
            _optionList.Add(Option);
            Option.GetComponentInChildren<TMP_Text>().text = option.Name;

            Button button = Option.GetComponent<Button>();
            button.name = option.Name;
            button.interactable = option.CheckConditions();

            button.onClick.AddListener(closeOptions);
            button.onClick.AddListener(option.Click);

            Option.transform.SetParent(_window.transform);
            center();
        }
    }
    void center()
    {
        if (_optionList.Count == 0) return;

        for (int i = 0; i < _optionList.Count; i++)
        {
            Vector3 pos = _optionPose.position;
            pos.y += _spacing * i;
            _optionList[i].transform.position = pos;
        }

        float center = (_optionList[_optionList.Count - 1].transform.position.y - _optionPose.position.y) / 2;

        foreach (var item in _optionList)
        {
            item.transform.Translate(0, -center, 0);
        }
    }
}
