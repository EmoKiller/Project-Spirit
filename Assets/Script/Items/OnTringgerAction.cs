using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTringgerAction : MonoBehaviour
{
    protected string text = "";
    protected bool actioned = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!actioned)
        {
            EventDispatcher.AddListener(Events.OnPlayerActionItems, ItemAction);
            EventDispatcher.TriggerEvent(Events.OnTriggerItems);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        EventDispatcher.RemoveListener(Events.OnPlayerActionItems, ItemAction);
    }
    public virtual void ItemAction()
    {
    }
}
