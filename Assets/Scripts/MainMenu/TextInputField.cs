using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputField : CustomInputField
{
    public Color defaultColor;
    public Color hoverColor;
    public int maxChar = -1;

    private Renderer rend;

    public GameObject keyboard;

    void Start ()
    {
        rend = GetComponent<Renderer>();
    }

    public override void Click()
    {
        Debug.Log("Click @StartButton");
        keyboard.SetActive(true);
    }

    public override void Hover()
    {
        Debug.Log("Hovered @StartButton");
        rend.material.SetColor("_Color", hoverColor);
    }

    public override void HoverExit()
    {
        Debug.Log("Hovered EXIT @StartButton");
        rend.material.SetColor("_Color", defaultColor);
    }
}
