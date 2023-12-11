using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DropAniDotWeen : MonoBehaviour
{
    [Header("Tranform DOMoveY")]
    public float MoveY = 1;
    public float TimeMoveY = 1;
    [Header("SetEase")]
    public Ease EaseType;
    [SerializeField]protected bool active = false;
    protected virtual void EventOnTrigger()
    {
        transform.DOMoveY(MoveY, TimeMoveY).SetEase(EaseType).OnComplete(() =>
        {
            active = true;
        });
    }
    private void Update()
    {
        if (!active)
            return;
        Event();
    }
    protected virtual void Event()
    {

    }
}
