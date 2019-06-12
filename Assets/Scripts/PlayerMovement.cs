using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 10;
	public float direction;
	public SteamVR_TrackedController VRcontroller;
	public GetVRInput VRinput;
    public GameObject eye;
    private Rigidbody localPlayer;

	// Use this for initialization
	void Start ()
    {
        localPlayer = GameObject.Find("LocalPlayer").GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        localPlayer.velocity = new Vector3(0, localPlayer.velocity.y, 0);
        if (Input.GetKey(KeyCode.W))
        {
            direction = eye.transform.rotation.eulerAngles.y / 180 * Mathf.PI;
            localPlayer.velocity = new Vector3(Mathf.Sin(direction) * moveSpeed, localPlayer.velocity.y, Mathf.Cos(direction) * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction = (eye.transform.rotation.eulerAngles.y + 90) / 180 * Mathf.PI;
            localPlayer.velocity = new Vector3(Mathf.Sin(direction) * moveSpeed, localPlayer.velocity.y, Mathf.Cos(direction) * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction = (eye.transform.rotation.eulerAngles.y + 180) / 180 * Mathf.PI;
            localPlayer.velocity = new Vector3(Mathf.Sin(direction) * moveSpeed, localPlayer.velocity.y, Mathf.Cos(direction) * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction = (eye.transform.rotation.eulerAngles.y + 270) / 180 * Mathf.PI;
            localPlayer.velocity = new Vector3(Mathf.Sin(direction) * moveSpeed, localPlayer.velocity.y, Mathf.Cos(direction) * moveSpeed);
        }

        if (VRinput != null)
        {
            if (VRinput.Touchpad())
            {
                print("Walking!");
                print(Mathf.Atan2(VRinput.touchValue.y, VRinput.touchValue.x));
                direction = (eye.transform.rotation.eulerAngles.y / 180 * Mathf.PI) - Mathf.Atan2(VRinput.touchValue.y, VRinput.touchValue.x) + Mathf.PI / 2;
                localPlayer.velocity = new Vector3(Mathf.Sin(direction) * moveSpeed, localPlayer.velocity.y, Mathf.Cos(direction) * moveSpeed);
            }
        }
    }
}
