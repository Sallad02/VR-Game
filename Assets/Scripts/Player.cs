using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int id = -1;
    private string playerName;
    public string PlayerName { get { return playerName; } set { playerName = value; Invoke("UpdateCanvas", .1f); } }

    public Transform head;
    public Transform body;
    public Transform VRControllerModel;

    public Text nameText;
    private RaycastLaser raycastLaser;

    private void Start()
    {
        if (transform.name != "LocalPlayer")
        {
            VRControllerModel.gameObject.SetActive(false);

            nameText = GetComponentInChildren<Text>();
        }
        raycastLaser = gameObject.GetComponentInChildren<RaycastLaser>();
    }

    public void MovePlayer(Vector3 position, Vector3 rotation)
    {
        this.transform.position = position;
        this.body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, rotation.y, body.transform.eulerAngles.z);
        this.head.transform.eulerAngles = rotation;
    }

    public void DrawLaser(Vector3 pointerPosition, Vector3 pointerRotation)
    {
        this.VRControllerModel.gameObject.SetActive(true);
        this.VRControllerModel.position = pointerPosition;
        this.VRControllerModel.eulerAngles = pointerRotation;

        RaycastHit? hit = raycastLaser.RaycastAndDrawLaser(this.VRControllerModel.transform);
    }

    public void HideLaser()
    {
        VRControllerModel.gameObject.SetActive(false);
        raycastLaser.HideLaser();
    }

    public void UpdateCanvas()
    {
        if(nameText != null)
            nameText.text = playerName;
    }

}
