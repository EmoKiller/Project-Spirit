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
    public List<ObjDropHeart> ObjDropHeart = new List<ObjDropHeart>();
    public List<ObjDropExp> ObjDropExp = new List<ObjDropExp>();
    public List<ObjDropCoin> ObjDropCoins = new List<ObjDropCoin>();
    public List<ObjDropAngry> ObjDropAngry = new List<ObjDropAngry>();
    public Dictionary<ChestType, ChestBonus> ObjectChestBonus = new Dictionary<ChestType, ChestBonus>();

    

    //[Button]
    //private void DropHeart(EnemGrPriteHeart TypeHeart)
    //{
    //    ObjDropHeart obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropHeart, "ObjDropHeart");
    //    obj.UpdateSprite(TypeHeart.ToString());
    //    obj.TypeHeart = TypeHeart;
    //    obj.Show();
    //}
    //[Button]
    //private void DropExp()
    //{
    //    ObjDropExp obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropExp, "ObjDropExp");
    //    obj.Show();
    //    //Instantiate(obj);
    //}
    //[Button]
    //private void DropCoin()
    //{
    //    ObjDropCoin obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropCoins, "ObjDropCoin");
    //    obj.Show();
    //}
    //[Button]
    //private void DropAngry()
    //{
    //    ObjDropAngry obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjDropAngry, "ObjDropAngry");
    //    obj.Show();
    //}
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
