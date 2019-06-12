using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuClickHandler : MonoBehaviour
{
    private CustomClient customClient;

    void Start()
    {
        customClient = GameObject.Find("Players").GetComponent<CustomClient>();
    }

    public void ClickHomeButton()
    {
        Debug.Log("Click");
        customClient.CloseConnection();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
