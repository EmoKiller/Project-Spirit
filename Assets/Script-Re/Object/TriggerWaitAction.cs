using UnityEngine;
using UnityEngine.UIElements;

public class TriggerWaitAction : MonoBehaviour
{
    public enum Script
    {
        TriggerWaitAction,
        TriggerTalk
    }
    public string text = "";
    public TypeShowButton typeButton;
    [SerializeField]protected bool actioned = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (actioned)
            return;
        if (typeButton == TypeShowButton.None)
        {
            OnTringgerWaitAction();
            EventDispatcher.Addlistener(Script.TriggerWaitAction, Events.OnTringgerWaitAction, OnTringgerWaitAction);
            return;
        }
        EventDispatcher.Addlistener(Script.TriggerWaitAction, Events.OnTringgerWaitAction, OnTringgerWaitAction);
        EventDispatcher.Publish(UIButtonAction.Script.UIButtonAction, Events.UIButtonOpen, typeButton, text);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        EventDispatcher.Publish(UIButtonAction.Script.UIButtonAction, Events.UIButtonReset);
    }
    protected virtual void OnTringgerWaitAction()
    {
        
    }
}
