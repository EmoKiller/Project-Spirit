using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TestAction : OnTringgerAction
{
    [SerializeField] GameObject obj;
    bool isOpen = false;
    [SerializeField] UnityEvent push;
    protected override void OnTriggerExit(Collider other)
    {
        if (isOpen)
            return;
        TringgerAction();
    }
    private void OnTriggerStay(Collider other)
    {
        push?.Invoke();
    }
    protected override void TringgerAction()
    {
        events?.Invoke();
        obj.gameObject.SetActive(true);
        isOpen = true;
    }
}
