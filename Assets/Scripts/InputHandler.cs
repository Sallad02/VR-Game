using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private SteamVR_Controller.Device controllerRight;
    private SteamVR_Controller.Device controllerLeft;

    private delegate void InputAction();
    private InputAction left;
    private InputAction right;
    private InputAction up;
    private InputAction down;
    private InputAction laser;
    private InputAction teleport;
    private InputAction select;


    /// <summary>
    /// Adds method to call when key is pressed
    /// This does in theory work for controllers as well as keyboard input.
    /// </summary>
    /// <param name="input">What input triggers action</param>
    /// <param name="action">What action to perform eg. delegate { Code Here... }</param>
    public void RegisterInputMethod(InputKey input, Action func)
    {
        InputAction inputAction = new InputAction(func);
        switch (input)
        {
            case InputKey.Left:
                left += inputAction;
                break;
            case InputKey.Right:
                right += inputAction;
                break;
            case InputKey.Up:
                up += inputAction;
                break;
            case InputKey.Down:
                down += inputAction;
                break;
            case InputKey.Laser:
                laser += inputAction;
                break;
            case InputKey.Teleport:
                teleport += inputAction;
                break;
            case InputKey.Select:
                select += inputAction;
                break;
        }
    }

    // Use this for initialization
    void Start ()
    {
        left = right = up = down = laser = teleport = select = new InputAction(delegate { }); // Clear input action
        if(transform.Find("Controller (left)"))
            controllerLeft = SteamVR_Controller.Input((int)transform.Find("Controller (left)").GetComponent<SteamVR_TrackedObject>().index);
        if(transform.Find("Controller (right)"))
            controllerRight = SteamVR_Controller.Input((int)transform.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>().index);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.A) || controllerLeft != null && controllerLeft.GetAxis().x < 0.0f)
        {
            left.Invoke();
        }
        if (Input.GetKey(KeyCode.D) || controllerLeft != null && controllerLeft.GetAxis().x > 0.0f)
        {
            right.Invoke();
        }
        if (Input.GetKey(KeyCode.W) || controllerLeft != null && controllerLeft.GetAxis().y > 0.0f)
        {
            up.Invoke();
        }
        if (Input.GetKey(KeyCode.S) || controllerLeft != null && controllerLeft.GetAxis().y < 0.0f)
        {
            down.Invoke();
        }
        if (Input.GetMouseButton(1) || controllerLeft != null && controllerRight.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            laser.Invoke();
        }
        if (Input.GetKey(KeyCode.E) || controllerLeft != null && controllerRight.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            select.Invoke();
        }
        if (Input.GetKey(KeyCode.T) || controllerLeft != null && controllerLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            teleport.Invoke();
        }
    }
}

public enum InputKey
{
    Left,
    Right,
    Up,
    Down,
    Laser,
    Teleport,
    Select
}
