using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : CustomButton {

    public Color defaultColor;
    public Color hoverColor;

    private Renderer rend;
    private MapButtonSelection mapButtonSelection;
    public Text inputName;
    private Text errorMessage;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mapButtonSelection = GameObject.Find("MapButtons").GetComponent<MapButtonSelection>();
        errorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();
    }

    public override void Click()
    {
        Debug.Log("Click @StartButton");
        if (mapButtonSelection.selected != null)
        {
            if (inputName.text != "")
            {
                VariableStorage.PlayerName = inputName.text;
            }
            else
            {
                //SET DEFAULT/RANDOM NAME
                System.Random rand = new System.Random();
                int randInt = rand.Next(1, 6);

                if (randInt == 1)
                {
                    VariableStorage.PlayerName = "Snabba Geparden^";
                }
                else if (randInt == 2)
                {
                    VariableStorage.PlayerName = "Långa Giraffen^";
                }
                else if (randInt == 3)
                {
                    VariableStorage.PlayerName = "Långsamma Snigeln^";
                }
                else if (randInt == 4)
                {
                    VariableStorage.PlayerName = "Hungriga Björnen^";
                }
                else if (randInt == 5)
                {
                    VariableStorage.PlayerName = "Stora Elefanten^";
                }
            }

            //SET SPAWNING COORDS(NOT USED RIGHT NOW)
            VariableStorage.PlayerSpawnX = mapButtonSelection.selected.GetComponent<MapButton>().SpawnCoordX;
            VariableStorage.PlayerSpawnY = mapButtonSelection.selected.GetComponent<MapButton>().SpawnCoordY;

            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else
        {
            errorMessage.text = "Choose a location first";
        }
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
