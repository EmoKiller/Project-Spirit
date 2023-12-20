using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDropCoin : ObjectDropOnWorld, IPool
{
    [Header("TypeHeart")]
    public TypeCoins TypeCoins;

    public string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        //InfomationPlayerManager.Instance.AttributeOnChange(AttributeType.CurrentCoin,(float)TypeCoins);
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
