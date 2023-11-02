using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPenaltyPedestal : OnTringgerWaitAction
{
    public float num = 0;
    private void Awake()
    {
        text = "Knell to be Sacrificed";
        typeButton = "Button";
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        EventDispatcher.Addlistener<float>(ListScript.DeathPenaltyPedestal,Events.UpdateValue, UpdateValue);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
    public void UpdateValue(float value)
    {
        num = value;
    }
    protected override void OnTringgerActionItems()
    {
        if (actioned)
            return;
        if (num >= 1)
        {
            EventDispatcher.Publish(ListScript.IntroGame, Events.AddListener);
            EventDispatcher.Publish(ListScript.IntroGame, Events.OpenBoxTalk);
            actioned = true;
            RemoveEvents();
        }
    }
}
