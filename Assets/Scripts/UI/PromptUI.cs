using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{
    [SerializeField] TMP_Text control, prompt;

    [SerializeField] Sprite selectedImg, commonImg;

    [SerializeField] Image img;

    public void SetText(string prompt)
    {
        this.prompt.text = prompt;
        control.text = InputManager.Input.Player.Interact.controls[0].name.ToUpper();
    }

    public void Select(bool isSelected)
    {
        img.sprite = (isSelected) ? selectedImg : commonImg;
    }
    public void Enabled(bool enabled)
    {
        Color color = enabled ? Color.white : Color.gray;
        control.color = color;
        prompt.color = color;
        img.color = color;
    }
}
