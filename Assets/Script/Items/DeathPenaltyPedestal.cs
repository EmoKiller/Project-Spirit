using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPenaltyPedestal : OnTringgerAction
{
    public float num = 0;
    private void Awake()
    {
        text = "Knell to be Sacrificed";
        imageButton = "Button";
    }
    public override void ItemAction()
    {
        
    }
}
