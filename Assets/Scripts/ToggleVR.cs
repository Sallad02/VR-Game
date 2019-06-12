using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
using UnityEngine;
using Valve.VR;

public class ToggleVR : MonoBehaviour
{
    GameObject standardCamera;
    GameObject vrCamera;

    GameObject controller1;
    GameObject controller2;

    GameObject VRview;
	GameObject MonitorView;

    //public GameObject head;

    //public SteamVR_TrackedObject TrackedObject;

    // Use this for initialization 


    ///Pretty ok version
    //IEnumerator Start ()
    //{
    //       controller1 = GameObject.Find("Controller (right)");
    //       controller2 = GameObject.Find("Controller (left)");
    //       controller1.SetActive(false);
    //       controller2.SetActive(false);
    //       print("State is: " + UnityEngine.XR.XRSettings.isDeviceActive);
    //       print("Name: " + UnityEngine.XR.XRSettings.loadedDeviceName);
    //       yield return new WaitForSeconds (1.5f);
    //       print("State is: " + UnityEngine.XR.XRSettings.isDeviceActive);
    //       //GameObject.Find ("VRView").SetActive(false);
    //       standardCamera = GameObject.Find("MonitorView");
    //	vrCamera = GameObject.Find("VRView");
    //	if (head.activeSelf) {
    //		print ("6787y");
    //		standardCamera.SetActive (true);
    //		vrCamera.SetActive (false);
    //		UnityEngine.XR.XRSettings.enabled = false;
    //	}
    //       else
    //       {
    //		print ("dgffh");
    //		vrCamera.SetActive (true);
    //		standardCamera.SetActive (false);
    //		UnityEngine.XR.XRSettings.enabled = true;
    //	}

    //}

    private void Start()
    {
        GameObject VRview = GameObject.Find("VRView");
        GameObject MonitorView = GameObject.Find("MonitorView");
        
        print("State is: " + UnityEngine.XR.XRSettings.isDeviceActive);
        if(!UnityEngine.XR.XRSettings.isDeviceActive)
        {
            print("We're here!");
            MonitorView.SetActive(true);
            VRview.SetActive(false);
            UnityEngine.XR.XRSettings.enabled = false;
            
        }
        else
        {
            MonitorView.SetActive(false);
            VRview.SetActive(true);
            UnityEngine.XR.XRSettings.enabled = true;
        }

        Player localPlayer = VRview.GetComponentInParent<Player>();
        localPlayer.head = localPlayer.GetComponentInChildren<Camera>().transform;
    }

    void Awake()
	{

    }

	// Update is called once per frame
	private void Update()
	{
		/*if (TrackedObject.origin != null) {
			vrCamera.SetActive (true);
			standardCamera.SetActive (false);
			UnityEngine.XR.XRSettings.enabled = true;
		}*/

		//If V is pressed, toggle VRSettings.enabled
		//if (Input.GetKeyDown(KeyCode.V))
		//{
		//	UnityEngine.XR.XRSettings.enabled = !UnityEngine.XR.XRSettings.enabled;
  //          vrCamera.SetActive(!vrCamera.activeSelf);
  //          standardCamera.SetActive(!standardCamera.activeSelf);
		//	Debug.Log("VR enabled:" + UnityEngine.XR.XRSettings.enabled);
		//}
	}
}
