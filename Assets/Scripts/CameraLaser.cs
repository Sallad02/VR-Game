using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaser : MonoBehaviour {

	//public GameObject laserPrefab;
	//public GameObject laserPointPrefab;
	//public GameObject laser;
	//private GameObject laserPoint;
	//private Transform laserTransform;
	//private Transform laserPointTransform;
	//private Vector3 hitPoint;

    //public DataLoader dataLoader;
    public BuildingInfo buildingInfo;
    public GameObject infoMenu;
    string hitName;
    private Transform lastHit = null;

    public RaycastLaser rayLaser;


    //private void DisplayLaser(RaycastHit laserTarget)
    //{
    //	laser.SetActive(true);
    //	laserPoint.SetActive(true);
    //	laserTransform.position = Vector3.Lerp(transform.position, hitPoint, 0.5f);
    //	laserTransform.LookAt(hitPoint);
    //	laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, laserTarget.distance);
    //	laserPointTransform.position = Vector3.Lerp(transform.position, hitPoint, 0.99f);
    //	//laserPointTransform.LookAt(transform.position);
    //	laserPointTransform.up = transform.position - hitPoint;
    //}

    ////Don't display dot if nothing is hit...
    //private void DisplayLaser(Vector3 direction)
    //{
    //	laser.SetActive(true);
    //	laserPoint.SetActive(true);
    //	laserTransform.position = transform.position;
    //	laserTransform.rotation = transform.rotation;
    //	laserTransform.Translate(new Vector3(0, 0, 50), Space.Self);
    //	laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, 100);
    //}
    void Start()
    {
        //infoMenu = GameObject.Find("InfoMenu");
    }
    void Awake()
	{
        infoMenu.SetActive(false);
        rayLaser = GetComponent<RaycastLaser>();
		//laser = Instantiate(laserPrefab);
		//laserPoint = Instantiate(laserPointPrefab);
		//laser.SetActive(false);
		//laserPoint.SetActive(false);
		//laserTransform = laser.transform;
		//laserPointTransform = laserPoint.transform;
	}

	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RaycastHit? hit = rayLaser.RaycastAndDrawLaser(transform);

            //Get building data from database if the laser hits a building with data.
            if (hit != null && hit.Value.transform.tag == "Building" && Input.GetKeyDown(KeyCode.E))
            {
                infoMenu.gameObject.SetActive(true);
                if (hitName != hit.Value.transform.name)
                {
                    hitName = hit.Value.transform.name;
                    StartCoroutine(buildingInfo.GetInfo(hit.Value.transform.GetComponent<BuildingID>().id));
                }
            }
            else if(Input.GetKeyDown(KeyCode.E) && infoMenu.activeSelf)
            {
                infoMenu.SetActive(false);
                hitName = "";
                buildingInfo.ClearText();
            }


            if (hit != null && hit.HasValue)
            {
                if (hit.Value.transform.tag == "Interactable")
                {
                    var interactable = hit.Value.transform.gameObject.GetComponent<Interactable>();
                    if (Input.GetKeyDown(KeyCode.E)) //If MapButton Hover and Trigger pressed
                    {
                        interactable.Click(); //MapButton Clicked
                    }

                    interactable.Hover();

                }


                if (lastHit != null)
                {
                    if (hit.Value.transform != lastHit && lastHit.tag == "Interactable")
                    {
                        var interactable = lastHit.transform.gameObject.GetComponent<Interactable>();
                        interactable.HoverExit();
                    }
                }
                lastHit = hit.Value.transform;
            }

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                infoMenu.SetActive(false);
                hitName = "";
                buildingInfo.ClearText();
            }
            rayLaser.HideLaser();
        }




 //           if (Input.GetKey(KeyCode.Mouse0))
		//{
		//	//RaycastHit hit;

  //          RaycastHit? hit = rayLaser.RaycastAndDrawLaser(transform);

  //          if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.transform.tag == "Building")
  //          {
  //              //BuildingData.GetData(hit.transform.GetComponent<BuildingID>().id);
                
  //              StartCoroutine(dataLoader.GetData(1));
                
  //          }

  //              if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
		//	{
		//		//print (hit.transform.name);
		//		if (hit.transform.tag == "Button")
		//		{
		//			//hit.transform.GetComponent<MainMenu>().ButtonHover();
		//		}

		//		hitPoint = hit.point;
		//		DisplayLaser(hit);
		//	}
		//	else
		//	{
		//		DisplayLaser(transform.rotation.eulerAngles);
		//	}

		//}

		//else
		//{
		//	laser.SetActive(false);
		//	laserPoint.SetActive(false);
		//}
	}
}
