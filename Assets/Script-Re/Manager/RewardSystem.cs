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
    public void DropHeart(EnemGrPriteHeart TypeHeart)
    {
        ObjDropHeart obj = ObjectPooling.Instance.PopDropHeart();
        obj.UpdateSprite(TypeHeart.ToString());
        obj.TypeHeart = TypeHeart;
        obj.Show();
    }
    [Button]
    public void DropExp(Vector3 vec3)
    {
        ObjDropExp obj = ObjectPooling.Instance.PopObjDropExp();
        obj.transform.position = vec3;
        obj.transform.AniDropItem();
        obj.Show();
    }
    [Button]
    public void DropCoin(Vector3 vec3)
    {
        ObjDropCoin obj = ObjectPooling.Instance.PopObjDropCoins();
        obj.transform.position = vec3;
        obj.transform.AniDropItem();
        obj.Show();
    }
    [Button]
    public void DropAngry(Vector3 vec3)
    {
        ObjDropAngry obj = ObjectPooling.Instance.PopObjDropAngry();
        obj.transform.position = vec3;
        obj.transform.AniDropItem();
        obj.Show();
    }
    [Button]
    public void SpawnChestBonus(ChestType type)
    {
        //ChestBonus obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjectChestBonus,"");
    }
    [Button]
    public void DropTarotCard(Vector3 vec3)
    {
        ObjDropTarotCard obj = ObjectPooling.Instance.PopObjDropTarotCard();
        obj.transform.position = vec3;
        obj.transform.AniDropItem();
        obj.Show();
    }
}
