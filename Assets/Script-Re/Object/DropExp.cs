using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropExp : DropAniDotWeen
{
    public EnemGrPriteHeart Type;
    private void Start()
    {
        //Transform player = (Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform);
        EventOnTrigger();
    }
    protected override void Event()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.CreateNewHeart, Type);
    }
}
