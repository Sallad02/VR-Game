using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLaser : MonoBehaviour
{
    private GameObject laserHolder;
	public GameObject LaserInstance { get; private set; }
	public GameObject LaserPointInstance { get; private set; }

	// Use this for initialization
	void Start () 
	{
		laserHolder = GameObject.Find("LaserHolder");
		LaserInstance = Instantiate(Resources.Load("Prefabs/Laser") as GameObject);
		LaserInstance.transform.SetParent(laserHolder.transform);
		LaserInstance.SetActive(false);
		LaserPointInstance = Instantiate(Resources.Load("Prefabs/LaserPoint") as GameObject);
		LaserPointInstance.transform.SetParent(laserHolder.transform);
		LaserPointInstance.SetActive(false);
	}

	public RaycastHit? RaycastAndDrawLaser(Transform origin)
	{
        RaycastHit? res = null;
		RaycastHit hit;
        float distance = 100;

		if (Physics.Raycast(origin.position, origin.forward, out hit, Mathf.Infinity)) 
		{
			DisplayLaserPoint(origin.position, hit);
            distance = hit.distance;
            res = hit;
		} 
        else
            LaserPointInstance.SetActive(false);

        DisplayLaser(origin, distance);

		return res;
	}

    private void DisplayLaser(Transform origin, float distance)
	{
        LaserInstance.SetActive(true);
        LaserInstance.transform.position = origin.position;
        LaserInstance.transform.rotation = origin.rotation;
        LaserInstance.transform.Rotate(90, 0, 0);
        LaserInstance.transform.localScale = new Vector3(LaserInstance.transform.localScale.x, distance/2, LaserInstance.transform.localScale.z);
        LaserInstance.transform.Translate(new Vector3(0, distance / 2, 0), Space.Self);
    }

    private void DisplayLaserPoint(Vector3 source, RaycastHit hit)
	{
		LaserPointInstance.SetActive(true);
		LaserPointInstance.transform.position = Vector3.Lerp(source, hit.point, 0.99f);
		LaserPointInstance.transform.up = source - hit.point;
	}

	public void HideLaser()
	{
		LaserPointInstance.SetActive(false);
		LaserInstance.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
        //HideLaser();
	}
}
