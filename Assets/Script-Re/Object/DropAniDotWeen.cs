using DG.Tweening;
using UnityEngine;

public class DropAniDotWeen : MonoBehaviour
{
    [Header("Tranform DOMoveY")]
    public float MoveY = 1;
    public float TimeMoveY = 1;
    [Header("SetEase")]
    public Ease EaseType;
    [Header("MoveToPlayer")]
    public float MoveSpeed = 0.5f;
    [Header("Distance")]
    public float Distance = 0.5f;
    bool active = false;
    protected virtual void EventOnTrigger(Transform other)
    {
        transform.DOMoveY(MoveY, TimeMoveY).SetEase(EaseType).OnComplete(() =>
        {
            var moteTween = transform.DOMove(other.position, MoveSpeed, false);

            moteTween.OnUpdate(() =>
            {
                var moteTweens = moteTween.ChangeEndValue(other.position, true);
                if (Vector3.Distance(transform.position, other.position) < Distance)
                {
                    moteTween.Pause();
                    if (active == true)
                        return;
                    Event();
                    active = true;
                    gameObject.SetActive(false);

                }
            });
        });
    }
    protected virtual void Event()
    {

    }
}
