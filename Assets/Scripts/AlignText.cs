using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignText : MonoBehaviour
{
    GameObject localPlayer;
    Canvas canvas;

	// Use this for initialization
	void Start ()
    {
        localPlayer = GameObject.Find("LocalPlayer");
        canvas = transform.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if((localPlayer.transform.position - this.transform.position).magnitude < 90)
        {
            canvas.enabled = true;
            transform.LookAt(localPlayer.transform);
        }
        else
        {
            canvas.enabled = false;
        }
	}
}
