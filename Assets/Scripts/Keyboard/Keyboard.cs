using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public Text inputText;

    public bool capsLockOn = false;
    public bool capitalizeFirst = true;

    private KeyboardCapslockButton capsLock;

    private void Start()
    {
        capsLock = gameObject.GetComponentInChildren<KeyboardCapslockButton>();
        if (capitalizeFirst)
            capsLock.Click();
    }

    public void addLetter(string letter)
    {
        if (capsLockOn)
        {
            inputText.text += letter.ToUpper();
        }
        else
        {
            inputText.text += letter.ToLower();
        }
        if (capitalizeFirst && inputText.text.Length == 1)
            capsLock.Click();
    }

    public void removeLetter()
    {
        if (inputText.text != "")
        {
            inputText.text = inputText.text.Remove(inputText.text.Length - 1);

            if (capitalizeFirst && inputText.text == "")
                capsLock.Click();
        }
    }
}
