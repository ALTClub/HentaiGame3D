using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{
    [SerializeField] Text control, prompt;

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
}
