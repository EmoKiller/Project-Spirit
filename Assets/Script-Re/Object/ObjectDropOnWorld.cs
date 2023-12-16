using DG.Tweening;
using System;
using UnityEngine;

public class ObjectDropOnWorld : MonoBehaviour
{
    [Header("Tranform DOMoveY")]
    public float MoveY = 1;
    public float TimeMoveY = 1;
    [Header("SetEase")]
    public Ease EaseType;
    [Header("Active")]
    [SerializeField] protected bool Ontrigger = false;
    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRenderer;
    protected Action pubLish = null;
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
        spriteRenderer.sprite = AssetManager.Instance.SpriteAtlasItems.GetSprite(spriteName);
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
        transform.position = Vector3.LerpUnclamped(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position, 10 * Time.deltaTime);
        if (Vector3.Distance(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position) < 0.5f)
        {
            pubLish?.Invoke();
            gameObject.SetActive(false);
            
        }
    }
    protected virtual void PublishEvent()
    {
        
    }
}
