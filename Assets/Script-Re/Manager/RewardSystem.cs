using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : SerializedMonoBehaviour
{
    public static RewardSystem Instance = null;
    [SerializeField] private List<ObjectDropOnWorld> ObjectDropOnWorld = new List<ObjectDropOnWorld>();
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
    public void SpawnChestBonus(ChestType type)
    {
        //ChestBonus obj = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.ObjectChestBonus,"");
    }
    [Button]
    public void DropObject(TypeItemsCanDrop type , Vector3 vec3)
    {
        ObjectDropOnWorld obj = ObjectPooling.Instance.PopObjectDrop(type.ToString());
        obj.transform.SetParent(transform,true);
        obj.transform.position = vec3;
        obj.transform.AniDropItem();
        obj.Show();
        ObjectDropOnWorld.Add(obj);
    }
    public void RemoveFromListObj(ObjectDropOnWorld obj)
    {
        ObjectDropOnWorld.Remove(obj);
    }
}
