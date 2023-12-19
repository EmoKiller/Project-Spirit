using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RewardSystem : SerializedMonoBehaviour
{
    [Button]
    private void DropTarotCard()
    {
        //ObjDropHeart obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropHeart, "ObjDropHeart");
        //obj.UpdateSprite(TypeHeart.ToString());
        //obj.TypeHeart = TypeHeart;
        //obj.Show();
    }

    [Button]
    private void DropHeart(EnemGrPriteHeart TypeHeart)
    {
        ObjDropHeart obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropHeart, "ObjDropHeart");
        obj.UpdateSprite(TypeHeart.ToString());
        obj.TypeHeart = TypeHeart;
        obj.Show();
    }
    [Button]
    private void DropExp()
    {
        ObjDropExp obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropExp, "ObjDropExp");
        obj.Show();
        //Instantiate(obj);
    }
    [Button]
    private void DropCoin()
    {
        ObjDropCoin obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropCoins, "ObjDropCoin");
        obj.Show();
    }
    [Button]
    private void DropAngry()
    {
        ObjDropAngry obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropAngry, "ObjDropAngry");
        obj.Show();
    }
    private void ChestBonus()
    {
        ChestBonus obj = Resources.Load<ChestBonus>("Chests/Chest_Wood");
        Instantiate(obj);
    }
}
