using UnityEngine;

public class OnTringgerWaitAction : MonoBehaviour
{
    protected string text = "";
    protected string typeButton = "";
    protected bool actioned = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (actioned)
            return;
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.UpdateText,text);
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.AddListener);
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.SwitchImageButton, typeButton);
        EventDispatcher.Addlistener(ListScript.OnTringgerWaitAction, Events.OnTringgerActionItems, OnTringgerActionItems);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        RemoveEvents();
    }
    protected virtual void OnTringgerActionItems()
    {

    }
    protected void RemoveEvents()
    {
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.SetDefaultButton);
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.RemoveEvent);
        EventDispatcher.RemoveEvent(ListScript.OnTringgerWaitAction, Events.OnTringgerActionItems);
    }
}