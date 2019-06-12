
//Simon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour {
	Vector3 vectest;
    public bool cursorLock = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) && cursorLock)
        {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X");
			float rotationY = transform.localEulerAngles.x - Input.GetAxis ("Mouse Y");
			if (rotationY >= 90.0f && rotationY <= 180.0f) {
				rotationY = 90.0f;
			} else if (rotationY <= 270.0f && rotationY >= 180.0f) {
				rotationY = -90.0f;
			}
			transform.localEulerAngles = new Vector3 (rotationY, rotationX, 0);
		} else if (Input.GetMouseButtonUp (0)) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
