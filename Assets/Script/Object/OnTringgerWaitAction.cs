using UnityEngine;

public class OnTringgerWaitAction : MonoBehaviour
{
    public string text = "";
    public TypeShowButton typeButton;
    [SerializeField]protected bool actioned = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (actioned)
            return;
        //EventDispatcher.Addlistener(ListScript.OnTringgerWaitAction, Events.OnTringgerPlayer, OnTringgerPlayer);

        EventDispatcher.Publish(ListScript.UIButtonAction, Events.UpdateText,text);
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.AddListener);
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.SwitchImageButton, typeButton);
        
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.AddListener);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        RemoveEvents();
    }
    protected virtual void OnTringgerPlayer()
    {
        if (actioned)
            return;
    }
    protected void RemoveEvents()
    {
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.SetDefaultButton);
        EventDispatcher.Publish(ListScript.UIButtonAction, Events.RemoveEvent);
        //EventDispatcher.RemoveEvent(ListScript.OnTringgerWaitAction, Events.OnTringgerPlayer);
    }
}
