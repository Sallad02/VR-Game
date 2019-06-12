using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonSelection : MonoBehaviour
{
    public MapButton selected = null;

    void Start()
    {
    }

    public void select(MapButton mapButton)
    {
        if (selected != null)
        {
            selected.selection.SetActive(false);
            Debug.Log("set old icon inactive");
        }

        selected = mapButton;
        selected.selection.SetActive(true); //Look for inacticve children
        Debug.Log("set new icon active");
    }

    public string getSelected()
    {
        if (selected != null)
        {
            return selected.GetComponent<MapButton>().location;
        }
        else
        {
            return "";
        }
    }
}
