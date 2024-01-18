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
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UIButtonOpen, typeButton, text);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UIButtonReset);
        EventDispatcher.RemoveEvent(Script.TriggerWaitAction, Events.OnTringgerWaitAction);
    }
    protected virtual void OnTringgerWaitAction()
    {
        actioned = true;
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UIButtonReset);
    }
}
