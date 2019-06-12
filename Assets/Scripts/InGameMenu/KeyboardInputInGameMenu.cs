using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInputInGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;
    private mouselook mouseLookActive;

	// Use this for initialization
	void Start ()
    {
        mouseLookActive = GameObject.Find("LocalPlayer").GetComponentInChildren<mouselook>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            mouseLookActive.cursorLock = !mouseLookActive.cursorLock;
            inGameMenu.SetActive(!inGameMenu.activeSelf);
        }		
	}
}
