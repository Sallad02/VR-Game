using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuHomeButton : CustomButton
{
    public Color defaultColor;
    public Color hoverColor;

    public GameObject inGameMenu;

    private CustomClient customClient;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        customClient = GameObject.Find("Players").GetComponent<CustomClient>();
    }

    public override void Click()
    {
        print("Click @MapButton");

        customClient.CloseConnection();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
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
