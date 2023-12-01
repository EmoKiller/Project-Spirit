using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TestAction : MonoBehaviour
{
    [SerializeField] GameObject obj;
    bool isOpen = false;
    [SerializeField] UnityEvent push;
    protected void OnTriggerExit(Collider other)
    {
        if (isOpen)
            return;
        TringgerAction();
        EventDispatcher.Publish(IntroGame.Script.IntroGame, Events.EnemyGoToWayPoint);
    }
    private void OnTriggerStay(Collider other)
    {
        push?.Invoke();
    }
    protected void TringgerAction()
    {
        //events?.Invoke();
        obj.gameObject.SetActive(true);
        isOpen = true;
    }
}
