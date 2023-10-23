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
            Debug.Log(text);
            UIManager.UpdateStringButtonE.Invoke(text);
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
