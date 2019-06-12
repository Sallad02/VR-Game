using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : CustomButton
{
    public Color defaultColor;
    public Color hoverColor;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public override void Click()
    {
        print("Click @MapButton");
        Application.Quit();
    }

    public override void Hover()
    {
        print("Hover @MapButton");
        rend.material.SetColor("_Color", hoverColor);
    }

    public override void HoverExit()
    {
        print("HoverExit @MapButton");
        rend.material.SetColor("_Color", defaultColor);
    }
}
