using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoWait : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        EventDispatcher.Addlistener(ListScript.WhoWait, Events.TriggerAction, SetAni);
    }
    private void SetAni()
    {
        _animator.SetTrigger("Kill");
    }
    public void InvokeAction()
    {
        EventDispatcher.Publish(ListScript.WhoWait, Events.TriggerAction2);
        EventDispatcher.Publish(ListScript.VideoPlayer, Events.UpdateValue, true);
        Debug.Log("invoke");
    }
}
