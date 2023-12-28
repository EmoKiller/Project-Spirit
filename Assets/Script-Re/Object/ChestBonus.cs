using DG.Tweening;
using System;
using UnityEngine;

public class ChestBonus : MonoBehaviour, IPool
{
    public ChestType type;
    private ObjectAnimator ObjAnimator => GetComponentInChildren<ObjectAnimator>();

    public string objectName => type.ToString();

    public Action OnDropItems = null;
    private void Start()
    {
        ObjAnimator.ActionOne = DropItems;
    }
    private void DropItems()
    {
        foreach (var item in ConfigDataHelper.GameConfig.ChestConfig[type].itemsCanDrop)
        {
            for (int i = 0; i < item.Value.value; i++)
            {
                SwitchType(item.Value.type, transform.position);
            }
        }
    }
    private void SwitchType(TypeItemsCanDrop type, Vector3 vec3)
    {
        switch (type)
        {
            case TypeItemsCanDrop.ObjDropCoin:
                RewardSystem.Instance.DropCoin(vec3); break;
            case TypeItemsCanDrop.ObjDropAngry:
                RewardSystem.Instance.DropAngry(vec3); break;
            case TypeItemsCanDrop.ObjDropTarotCard:
                RewardSystem.Instance.DropTarotCard(vec3); break;
        }
    }
    public void Show()
    {
        throw new NotImplementedException();
    }

    public void Hide()
    {
        throw new NotImplementedException();
    }
}
