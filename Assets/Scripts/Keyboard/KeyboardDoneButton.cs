using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardDoneButton : CustomButton
{
    public Color defaultColor;
    public Color hoverColor;
    public string letter;
    public Text letterText;

    public Keyboard keyboard;

    private Renderer rend;


    void Start()
    {
        rend = GetComponent<Renderer>();
        letterText.text = letter;
    }

    public override void Click()
    {
        Debug.Log("CLICK @KeyboardLetterButton");
        keyboard.gameObject.SetActive(false);
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
