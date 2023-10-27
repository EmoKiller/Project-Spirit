using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPenaltyPedestal : OnTringgerAction
{
    float count = 0;
    private void Awake()
    {
        text = "Knell to be Sacrificed";
    }
    public override void ItemAction()
    {
        count += 0.0001f;
        UIManager.imageEvent?.Invoke(count);
        Debug.Log(count);
    }
}
