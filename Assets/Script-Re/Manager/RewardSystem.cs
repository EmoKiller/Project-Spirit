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
        ObjDropHeart obj = ObjectPooling.Instance.PopDropHeart("ObjDropHeart");
        obj.UpdateSprite(TypeHeart.ToString());
        obj.TypeHeart = TypeHeart;
        obj.Show();
    }
    [Button]
    private void DropExp()
    {
        ObjDropExp obj = ObjectPooling.Instance.PopObjDropExp("ObjDropExp");
        obj.Show();
        //Instantiate(obj);
    }
    [Button]
    private void DropCoin()
    {
        ObjDropCoin obj = ObjectPooling.Instance.PopObjDropCoins("ObjDropCoin");
        obj.Show();
    }
    [Button]
    private void DropAngry()
    {
        ObjDropAngry obj = ObjectPooling.Instance.PopObjDropAngry("ObjDropAngry");
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
        //ObjDropHeart obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropHeart, "ObjDropHeart");
        //obj.UpdateSprite(TypeHeart.ToString());
        //obj.TypeHeart = TypeHeart;
        //obj.Show();
    }
}
