using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVRInput : MonoBehaviour {
	public SteamVR_TrackedObject mTrackedObject = null;
	public SteamVR_Controller.Device mDevice;
    public Vector2 triggerValue;
    public Vector2 touchValue;
    public int asrgrgsdf = 0;

	public bool Touchpad ()
	{
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			print("Touchpad down");
			touchValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);	// gives X and Y value on the touchpad
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool Trigger (){
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
			print("Trigger down");
			triggerValue = Controller.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool Grip (){
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
			print ("grip down");
			return true;
		}
		else
		{
			return false;
		}
	}



    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)mTrackedObject.index); }
    }

    // Use this for initialization
    void Awake () {
		mTrackedObject = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {

        //Trigger
        
        /*if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
            touchValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        }
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
			
		}

		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			touchValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		}
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			touchValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			touchValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		}
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			touchValue = Controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
		}
        if (mDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
            print("Trigger down");
        }
		if (mDevice.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
			print ("Trigger up");
		}
		triggerValue = mDevice.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);


		//Grip
		if (mDevice.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			print ("grip down");
		}
		if (mDevice.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) {
			print ("grip up");
		}


		//touchpad
		if (mDevice.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			print("Touchpad down");
		}
		if (mDevice.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			print ("Touchpad up");
		}

		touchValue = mDevice.GetAxis (Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);*/
	}
}

