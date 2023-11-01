using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : OnTringgerWaitAction
{
    void Awake()
    {
        text = "Pick Berry";
        typeButton = "Mouse";
    }
    protected override void OnTringgerActionItems()
    {
        Debug.Log("BerryBush");
    }

}
