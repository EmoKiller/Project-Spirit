using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathPenaltyPedestal : OnTringgerWaitAction
{
    public float num = 0;
    [SerializeField] UnityEvent events;
    [SerializeField]List<string> list;
    [SerializeField] WayPoint waypoints;
    [SerializeField] private int count = 0;
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
    private void AddListenerBoxTalk()
    {
        EventDispatcher.Addlistener(ListScript.Talking, Events.OpenBoxTalk, TalkingText);
    }
    private void TalkingText()
    {
        if (count >= list.Count)
        {
            events?.Invoke();
            return;
        }
        EventDispatcher.Publish(ListScript.CameraFollow, Events.SetSmooth, 0.6f);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, waypoints.points[count]);
        EventDispatcher.Publish(ListScript.TalkTime, Events.OpenBoxTalk, list[count]);
        count++;
    }
    
    protected override void OnTringgerActionItems()
    {
        if (actioned)
            return;
        if (num >= 1)
        {
            actioned = true;
            RemoveEvents();
            AddListenerBoxTalk();
            EventDispatcher.Publish(ListScript.Talking, Events.OpenBoxTalk);
        }
    }
}
