using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDropCoin : ObjectDropOnWorld
{
    [Header("TypeHeart")]
    public TypeCoins TypeCoins;

    public override string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        InfomationPlayerManager.Instance.CurrentCoins += (int)TypeCoins;
    }

}
