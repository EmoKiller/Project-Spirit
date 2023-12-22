using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RewardSystem : SerializedMonoBehaviour
{
    public static RewardSystem Instance = null;
    private void Awake()
    {
        Instance = this;
    }


    [Button]
    private void DropHeart(EnemGrPriteHeart TypeHeart)
    {
        ObjDropHeart obj = ObjectPooling.Instance.PopDropHeart();
        obj.UpdateSprite(TypeHeart.ToString());
        obj.TypeHeart = TypeHeart;
        obj.Show();
    }
    [Button]
    private void DropExp()
    {
        ObjDropExp obj = ObjectPooling.Instance.PopObjDropExp();
        obj.Show();
        //Instantiate(obj);
    }
    [Button]
    private void DropCoin()
    {
        ObjDropCoin obj = ObjectPooling.Instance.PopObjDropCoins();
        obj.Show();
    }
    [Button]
    private void DropAngry()
    {
        ObjDropAngry obj = ObjectPooling.Instance.PopObjDropAngry();
        obj.Show();
    }
    [Button]
    private void SpawnChestBonus(ChestType type)
    {
        //ChestBonus obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjectChestBonus,"");
    }
    [Button]
    private void DropTarotCard()
    {
        ObjDropTarotCard obj = ObjectPooling.Instance.PopObjDropTarotCard();
        obj.Show();
    }
}
