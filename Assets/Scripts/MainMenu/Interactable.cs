using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public virtual void Click()
    {
        print("CLICK @ Interactable");
    }

    public virtual void Hover()
    {
    }

    public virtual void HoverExit()
    {
    }
}
