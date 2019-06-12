using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : CustomButton
{
    public bool isActive = true;
    public string location;
    public Color defaultColor;
    public Color hoverColor;
    public Color inactiveColor;
    public float SpawnCoordX;
    public float SpawnCoordY;
    public GameObject selection;

    private Renderer rend;
    private Text locationText;
    private MapButtonSelection mapButtonSelection;

    void Start()
    {
        rend = GetComponent<Renderer>();
        locationText = GameObject.Find("Location").GetComponent<Text>();
        mapButtonSelection = GameObject.Find("MapButtons").GetComponent<MapButtonSelection>();

        if (!isActive)
        {
            rend.material.SetColor("_Color", inactiveColor);
        }
    }

    public override void Click()
    {
        print("Click @MapButton");

        if (isActive)
        {
            mapButtonSelection.select(this.gameObject.GetComponent<MapButton>());
        }
    }

    public override void Hover()
    {
        print("Hover @MapButton");

        if (isActive)
        {
            rend.material.SetColor("_Color", hoverColor);
        }

        locationText.text = location;
    }

    public override void HoverExit()
    {
        print("HoverExit @MapButton");

        if (isActive)
        {
            rend.material.SetColor("_Color", defaultColor);
        }

        locationText.text = mapButtonSelection.getSelected();
    }

}
