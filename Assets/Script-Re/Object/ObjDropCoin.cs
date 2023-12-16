using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDropCoin : ObjectDropOnWorld, IPool
{
    [Header("TypeHeart")]
    public TypeCoins TypeCoins;

    public string objectName => GetType().ToString();

    private void Awake()
    {
        pubLish = PublishEvent;
    }
    protected override void PublishEvent()
    {
        //EventDispatcher.Publish(UIManager.Script.UIManager, Events.CreateNewHeart, TypeHeart);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    

}
