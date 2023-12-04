using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.tvOS;

public class TriggerTalk : TriggerWaitAction
{
    
    public TalkScript indexScript;
    public UnityEvent Event;
    private void Start()
    {
        EventDispatcher.Addlistener(Script.TriggerTalk,Events.TheScriptTalkEnd, TheScriptTalkEnd);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        
    }
    protected override void OnTringgerWaitAction()
    {
        if (actioned)
            return;
        EventDispatcher.Publish(UIDialogBox.Script.UIDialogBox, Events.DialogBoxChangeTalkScript, indexScript.ToString());
        EventDispatcher.Publish(UIButtonAction.Script.UIButtonAction, Events.UIButtonReset);
        actioned = true;
    }
    protected void TheScriptTalkEnd()
    {
        Event?.Invoke();
    }

}
