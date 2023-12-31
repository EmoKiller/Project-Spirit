using DG.Tweening;
using System;
using UnityEngine;

public class ObjectDropOnWorld : MonoBehaviour, IPool
{
    [Header("Tranform DOMoveY")]
    public float TimeMove = 10f;
    public float TimeMoveMulti = 1;
    public float MoveY = 1;
    public float TimeMoveY = 1;
    [Header("SetEase")]
    public Ease EaseType;
    [Header("Active")]
    [SerializeField] protected bool Ontrigger = false;
    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRenderer;
    protected Action pubLish = null;

    public virtual string objectName => GetType().Name;

    private void Awake()
    {
        pubLish = PublishEvent;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        EventOnTrigger();
    }
    public void UpdateSprite(string spriteName)
    {
        spriteRenderer.sprite = ObjectPooling.Instance.SpriteAtlasItems.GetSprite(spriteName);
    }
    protected virtual void EventOnTrigger()
    {
        if (Ontrigger == true)
            return;
        transform.DOMoveY(MoveY, TimeMoveY).SetEase(EaseType).OnComplete(() =>
        {
            Ontrigger = true;
        });
    }
    private void Update()
    {
        if (!Ontrigger)
            return;
        Event();
    }
    private void OnDisable()
    {
        Ontrigger = false;
    }
    protected virtual void Event()
    {
        if (Vector3.Distance(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position) < 1f)
        {
            TimeMoveMulti = 2;
        }
        transform.position = Vector3.LerpUnclamped(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position, TimeMove* TimeMoveMulti * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position) < 0.5f)
        {
            pubLish?.Invoke();
            Hide();
        }
    }
    protected virtual void PublishEvent()
    {
        
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        TimeMoveMulti = 1;
        RewardSystem.Instance.RemoveFromListObj(this);
        ObjectPooling.Instance.PushToPoolObjectDrop(this);
        gameObject.SetActive(false);
    }
}
