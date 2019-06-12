using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildingInfo : MonoBehaviour {

    public DataLoader dataLoader;
    private Text text;
    
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame


    //Get building data and update the info text.
    public IEnumerator GetInfo(int id)
    {
        Debug.LogWarning("Getting building info from database");
        StartCoroutine(dataLoader.GetData(id));
        yield return dataLoader.GetData(id);
        Debug.LogWarning("Info: " + dataLoader.data);
        text.text = dataLoader.data;
    }

    public void ClearText()
    {
        text.text = "";
    }
}
