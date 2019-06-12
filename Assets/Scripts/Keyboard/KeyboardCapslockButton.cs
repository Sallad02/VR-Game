using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardCapslockButton : CustomButton
{
    public Color defaultColor;
    public Color hoverColor;

    private Keyboard keyboard;

    private Renderer rend;

    private KeyboardLetterButton[] keyboardLetterButtons;


    void Start()
    {
        keyboard = GameObject.Find("Keyboard").GetComponent<Keyboard>();
        keyboardLetterButtons = keyboard.GetComponentsInChildren<KeyboardLetterButton>();
        rend = GetComponent<Renderer>();
    }

    public override void Click()
    {
        Debug.Log("CLICK @KeyboardLetterButton");

        keyboard.capsLockOn = !keyboard.capsLockOn;

        foreach (KeyboardLetterButton letterButton in keyboardLetterButtons)
        {
            letterButton.SetCapsLockMode(keyboard.capsLockOn);
        }

    }

    public override void Hover()
    {
        Debug.Log("Hovered @KeyboardLetterButton");
        rend.material.SetColor("_Color", hoverColor);
    }

    public override void HoverExit()
    {
        Debug.Log("Hovered EXIT @KeyboardLetterButton");
        rend.material.SetColor("_Color", defaultColor);
    }

}