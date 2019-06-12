using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButton : Interactable
{
    public override void Hover()
    {
        print("HOVER @CusomButton");
    }

    public override void HoverExit()
    {
        print("HOVER EXIT @CusomButton");
    }
}
