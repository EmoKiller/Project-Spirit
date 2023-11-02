using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkWhitTheWhoWaitsIntro : OnTringgerAction
{
    protected override void OnTriggerEnter(Collider other)
    {
        TringgerAction();
    }
    protected override void TringgerAction()
    {
        events?.Invoke();
    }
}
