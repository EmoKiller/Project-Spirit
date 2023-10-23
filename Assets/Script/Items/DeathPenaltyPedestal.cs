using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPenaltyPedestal : OnTringgerAction
{
    
    private void Awake()
    {
        text = "Knell to be Sacrificed";
    }
    public override void ItemAction()
    {
        Debug.Log(text.Length);
        actioned = true;
    }
}
