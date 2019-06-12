using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserPointer : MonoBehaviour
{
    public bool LaserActive { get; private set; }
    private SteamVR_TrackedObject trackedObj;
    private RaycastLaser raycastLaser;
    private RaycastHit? previousInteractable;

    public BuildingInfo buildingInfo;
    public GameObject inGameMenu;
    string hitName;

    public GameObject infoMenu;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Start()
    {

    }
    void Awake()
    {
        infoMenu = GameObject.Find("InfoMenu");
        infoMenu.SetActive(false);
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        raycastLaser = this.gameObject.GetComponent<RaycastLaser>();
        LaserActive = false;
    }

    private Transform lastHit = null;

    // Update is called once per frame
    void Update()
    {
        //Hover exit not working properly, get called each update
        if (previousInteractable != null && previousInteractable.Value.collider.transform.tag == "Interactable")
        {
            previousInteractable.Value.collider.transform.gameObject.GetComponent<Interactable>().HoverExit();
            previousInteractable = null;
        }
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            LaserActive = true;
            RaycastHit? hit = raycastLaser.RaycastAndDrawLaser(transform);


            if(hit != null && hit.Value.collider.transform.tag == "Interactable")
            {
                Interactable interactable = hit.Value.collider.transform.gameObject.GetComponent<Interactable>();
                if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    interactable.Click();
                else
                    interactable.Hover();

                previousInteractable = hit;
            }
        }
        else
        {
            raycastLaser.HideLaser();
            LaserActive = false;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if(SceneManager.GetActiveScene().name != "MainMenu")
                inGameMenu.SetActive(!inGameMenu.activeSelf);
        }
        

        //    //Get building data from database if the laser hits a building with data.
        //    if (hit != null && hit.Value.transform.tag == "Building" && Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //    {
        //        infoMenu.gameObject.SetActive(true);
        //        if (hitName != hit.Value.transform.name)
        //        {
        //            hitName = hit.Value.transform.name;
        //            StartCoroutine(buildingInfo.GetInfo(hit.Value.transform.GetComponent<BuildingID>().id));
        //        }

        //    }
        //    else
        //    {
        //        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //        {
        //            infoMenu.gameObject.SetActive(false);
        //            hitName = "";
        //            buildingInfo.ClearText();
        //        }
        //    }


        //    if (hit != null && hit.HasValue)
        //    {
        //        Debug.Log("HIT:" + hit.Value.transform.gameObject.name);
        //        if (hit.Value.transform.tag == "Interactable")
        //        {
        //            print("Is interactable");
        //            var interactable = hit.Value.transform.gameObject.GetComponent<Interactable>();

        //            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) //If MapButton Hover and Trigger pressed
        //            {
        //                print("Clicking interactable");
        //                interactable.Click(); //MapButton Clicked
        //            }
        //            else
        //            {
        //                print("Not click");
        //            }

        //            interactable.Hover();

        //        }


        //        if (lastHit != null)
        //        {
        //            if (hit.Value.transform != lastHit && lastHit.tag == "Interactable")
        //            {
        //                var interactable = lastHit.transform.gameObject.GetComponent<Interactable>();
        //                interactable.HoverExit();
        //            }
        //        }
        //        lastHit = hit.Value.transform;
        //    }
        //}
        //else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        //{
        //    hitName = "";
        //    //buildingInfo.ClearText();
        //    if (lastHit != null)
        //    {
        //        if (lastHit.tag == "Interactable")
        //        {
        //            var interactable = lastHit.transform.gameObject.GetComponent<Interactable>();
        //            interactable.HoverExit();
        //        }
        //    }
        //    raycastLaser.HideLaser();
        //}
        //else if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    infoMenu.gameObject.SetActive(false);
        //    hitName = "";
        //    buildingInfo.ClearText();
        //}
        //else
        //{
        //    raycastLaser.HideLaser(); // Might not be needed
        //}

        //if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        //{
        //    inGameMenu.SetActive(!inGameMenu.activeSelf);
        //}
    }
}
