using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInteractable : CustomButton
{
    public GameObject infoMenu;

    private BuildingInformation buildingInfo;

    void Start()
    {
        buildingInfo = GetComponent<BuildingInformation>();
    }

    public override void Click()
    {
        if (infoMenu.activeSelf)
        {
            Debug.Log("1");
            infoMenu.SetActive(false);
        }
        else
        {
            Debug.Log("2");
            StartCoroutine(infoMenu.GetComponentInChildren<BuildingInfo>().GetInfo(GetComponent<BuildingID>().id));
            //infoMenu.GetComponentInChildren<Text>().text = buildingInfo.buildingInformation;
            infoMenu.SetActive(true);
        }


        print("Click @BuildingInteractable");
        
    }

}
