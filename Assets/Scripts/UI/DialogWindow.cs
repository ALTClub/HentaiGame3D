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

    [Header("Задержки")]
    [SerializeField][Range(0, 0.5f)] private float charDelay = 0.05f;
    [SerializeField][Range(0, 1)] private float punctuationDelay;
    [SerializeField][Range(0, 1)] private float lineDelay;

    [Header("Варианты")]
    [SerializeField] GameObject _optionPrefab;
    [SerializeField] Transform optionPose;
    [SerializeField] float spacing;

    public static UnityEvent<Dialog> PlayDialog = new();
    public static UnityEvent<String, UnityEvent> Read = new();
    public static UnityEvent<List<Option>> SpawnOptions = new();
    public static UnityEvent Close = new();

    private void OnEnable()
    {
        PlayDialog.AddListener(Initialize);
        Read.AddListener(Reader);
        SpawnOptions.AddListener(spawnOptions);
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

        Cursor.lockState = CursorLockMode.None;
        dialog.Start();
        window.SetActive(true);
    }
    public void close()
    {
        InputManager.DisableControls();
        InputManager.EnableControls();
        Cursor.lockState = CursorLockMode.Locked;
        window.SetActive(false);
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
        OnReaded.Invoke();
    }
    List<GameObject> optionList = new();
    void closeOptions()
    {
        foreach (GameObject option in optionList) Destroy(option.gameObject);
        optionList.Clear();
    }
    void spawnOptions(List<Option> options)
    {
        foreach (var option in options)
        {
            option.Start(_name, _text);
            GameObject Option = Instantiate(_optionPrefab, transform.position, Quaternion.identity);
            optionList.Add(Option);
            Option.GetComponentInChildren<TMP_Text>().text = option.Name;

            Button button = Option.GetComponent<Button>();
            button.name = option.Name;
            button.interactable = option.CheckConditions();

            button.onClick.AddListener(closeOptions);
            button.onClick.AddListener(option.Click);

            Option.transform.SetParent(window.transform);
            center();
        }
    }
    void center()
    {
        if (optionList.Count == 0) return;

        for (int i = 0; i < optionList.Count; i++)
        {
            Vector3 pos = optionPose.position;
            pos.y += spacing * i;
            optionList[i].transform.position = pos;
        }

        float center = (optionList[optionList.Count - 1].transform.position.y - optionPose.position.y) / 2;

        foreach (var item in optionList)
        {
            item.transform.Translate(0, -center, 0);
        }
    }
}
