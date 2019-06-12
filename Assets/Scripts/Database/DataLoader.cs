using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataLoader : MonoBehaviour
{
    /*[HideInInspector]*/public string data;

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        return value;
    }
    
    //Get data from the data from the database.
    public IEnumerator GetData(int id)
    {
        //Gets data from the row with the id 'id'.
        WWW testData = new WWW("http://130.240.170.72/WS2K17/TestData.php?id=" + id.ToString());
        yield return testData;
        if(testData.text != "")
        {
            string testDataString = testData.text;
            data = GetDataValue(testDataString, "Info:");
        }
        else
        {
            Debug.LogWarning("Could not connect to database!");
        }
    }
}