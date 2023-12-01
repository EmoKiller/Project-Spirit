using UnityEngine;
using UnityEngine.tvOS;

public class TriggerTalk : TriggerWaitAction
{
    
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
        Debug.Log("Action");
        actioned = true;
    }

}
