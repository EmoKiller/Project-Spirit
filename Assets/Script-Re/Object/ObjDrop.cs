using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDrop : ObjectDropOnWorld
{
    public float Value = 1;
    public AttributeType AttributeType;
    private void OnEnable()
    {
        if(AttributeType == AttributeType.CurrentExp)
        {
            transform.DOMoveY(4, 1).OnComplete(() =>
            {
                Ontrigger = true;
            });
        }
    }
    protected override void PublishEvent()
    {
        if(AttributeType == AttributeType.CurrentExp)
        {
            InfomationPlayerManager.Instance.CurrentExp += Value;
            return;
        }
        if (AttributeType == AttributeType.CurrentAngry)
        {
            InfomationPlayerManager.Instance.CurrentAngry += Value;
            return;
        }
        InfomationPlayerManager.Instance.IncreaseValueOf(AttributeType,Value);
    }
}
