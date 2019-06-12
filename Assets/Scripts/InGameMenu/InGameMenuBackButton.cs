using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuBackButton : CustomButton
{
    public Color defaultColor;
    public Color hoverColor;

    public GameObject inGameMenu;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public override void Click()
    {
        print("Click @MapButton");
        inGameMenu.SetActive(false);
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
