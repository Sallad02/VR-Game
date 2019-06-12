using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardLetterButton : CustomButton
{
    public Color defaultColor;
    public Color hoverColor;
    public string letter;
    public Text letterText;

    private Keyboard keyboard;

    private Renderer rend;


    void Start()
    {
        keyboard = GameObject.Find("Keyboard").GetComponent<Keyboard>();
        rend = GetComponent<Renderer>();
        //letterText.text = letter;
    }

    public override void Click()
    {
        Debug.Log("CLICK @KeyboardLetterButton");
        keyboard.addLetter(letter);
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

    public void SetCapsLockMode(bool capsLockMode)
    {
        if (capsLockMode)
        {
            letterText.text = letter.ToUpper();
        }
        else
        {
            letterText.text = letter.ToLower();
        }
    }

}
