using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : SerializedMonoBehaviour
{
    //SpawnSystem
    public static RewardSystem Instance = null;
    [SerializeField] private List<EffectDestroyObject> effectDestroyObj = new List<EffectDestroyObject>();
    [SerializeField] private List<ObjectDropOnWorld> ObjectDropOnWorld = new List<ObjectDropOnWorld>();
    [SerializeField] private List<ImpactableObjects> impactableObjects = new List<ImpactableObjects>();
    [SerializeField] private List<ChestBonus> chestBonus = new List<ChestBonus>();
    [SerializeField] private List<ObjectSkill> objectSkill = new List<ObjectSkill>();
    [SerializeField] private List<ObjectSkill> objectSkillEnemy = new List<ObjectSkill>();
    [SerializeField] private List<ObjEffectAnimation> objEffectAnimation = new List<ObjEffectAnimation>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
    }
    [Button]
    
    public List<ImpactableObjects> CheckDeadHuman(Vector3 pos)
    {
        List<ImpactableObjects> obj2 = impactableObjects.FindAll(e => Vector3.Distance(pos, e.transform.position) < 30);
        return obj2;
    }
    [Button]
    public void DropHeart(EnemGrPriteHeart TypeHeart)
    {
        ObjDropHeart obj = ObjectPooling.Instance.PopDropHeart(true);
        obj.UpdateSprite(TypeHeart.ToString());
        obj.TypeHeart = TypeHeart;
    }
    public void SpawnEffectDestroyObj(ListTypeEffects type, Vector3 vec3, out EffectDestroyObject effectDestroy)
    {
        EffectDestroyObject obj = ObjectPooling.Instance.PopChestEffectDestroyObj(type.ToString());
        SetUpObj(effectDestroyObj, obj, vec3);
        effectDestroy = obj;
    }
    public void RemoveFromListEffectDestroyObj(EffectDestroyObject obj)
    {
        effectDestroyObj.Remove(obj);
    }
    [Button]
    public void SpawnChestBonus(ChestType type, Vector3 vec3, out ChestBonus chest)
    {
        ChestBonus obj = ObjectPooling.Instance.PopChestBonus(type.ToString());
        SetUpObj(chestBonus, obj, vec3);
        chest = obj;
    }
    public void RemoveFromListChestBonus(ChestBonus obj)
    {
        chestBonus.Remove(obj);
    }
    public void SpawnObjEffectAnimation(TypeEffectAnimation type, Vector3 vec3)
    {
        ObjEffectAnimation obj = ObjectPooling.Instance.PopObjEffectAnimation(type.ToString());
        SetUpObj(objEffectAnimation, obj, vec3);
    }
    public void RemoveFromListObjEffectAnimation(ObjEffectAnimation obj)
    {
        objEffectAnimation.Remove(obj);
    }
    [Button]
    public void DropObject(TypeItemsCanDrop type, Vector3 vec3)
    {
        ObjectDropOnWorld obj = ObjectPooling.Instance.PopObjectDrop(type.ToString());
        SetUpObj(ObjectDropOnWorld, obj, vec3);
        obj.Show();
        obj.transform.AniDropItem();
    }
    [Button]
    public void DropObject(TypeItemsCanDrop type, Vector3 vec3, out ObjectDropOnWorld objout)
    {
        ObjectDropOnWorld obj = ObjectPooling.Instance.PopObjectDrop(type.ToString());
        SetUpObj(ObjectDropOnWorld, obj, vec3);
        obj.Show();
        obj.transform.AniDropItem();
        objout = obj;
    }
    public void RemoveFromListObj(ObjectDropOnWorld obj)
    {
        ObjectDropOnWorld.Remove(obj);
    }


    public void DropImpactableObjects(string type, Vector3 vec3)
    {
        ImpactableObjects obj = ObjectPooling.Instance.PopImpactableObjects(type);
        SetUpObj(impactableObjects, obj, vec3);
    }
    public void RemoveFromListImpactableObj(ImpactableObjects obj)
    {
        impactableObjects.Remove(obj);
    }

    public void SpawnObjectSkill(string type, Vector3 vec3, out ObjectSkill outSkill)
    {
        ObjectSkill obj = ObjectPooling.Instance.PopChestObjectSkill(type);
        SetUpObj(objectSkill, obj, vec3);
        outSkill = obj;
    }
    public void RemoveFromListObjectSkill(ObjectSkill obj)
    {
        objectSkill.Remove(obj);
    }
    public void SpawnObjectSkillEnemy(string type, Vector3 vec3, out ObjectSkill outSkill)
    {
        ObjectSkill obj = ObjectPooling.Instance.PopChestObjectSkillEnemy(type);
        SetUpObj(objectSkillEnemy, obj, vec3);
        outSkill = obj;
    }
    public void RemoveFromListObjectSkillEnemy(ObjectSkill obj)
    {
        objectSkillEnemy.Remove(obj);
    }

    private void SetUpObj<T>(List<T> list, T obj, Vector3 vec3) where T : MonoBehaviour, IPool
    {
        obj.transform.SetParent(transform, true);
        obj.transform.position = vec3;
        obj.Show();
        list.Add(obj);
    }
}
