using UnityEngine;
using UnityEngine.tvOS;

public class DeathPenaltyPedestal : OnTringgerWaitAction
{
    public float num = 0;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //EventDispatcher.Addlistener<float>(ListScript.TypeButton,Events.UpdateValue, UpdateValue);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        //EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.RemoveEvent);
    }
    private void UpdateValue(float value)
    {
        num = value;
    }
    protected override void OnTringgerPlayer()
    {
        base.OnTringgerPlayer();
        if (num >= 1)
        {
            actioned = true;
            RemoveEvents();
            //EventDispatcher.Publish(ListScript.PopUpTalk, Events.AddListener);
            //EventDispatcher.Publish(ListScript.PopUpTalk, Events.OpenPopup);
        }
    }
}
