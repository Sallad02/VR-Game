using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputField : Interactable
{
    public override void Hover()
    {
        print("HOVER @CustomInputField");
    }

    public override void HoverExit()
    {
        print("HOVER EXIT @CustomInputField");
    }


}
