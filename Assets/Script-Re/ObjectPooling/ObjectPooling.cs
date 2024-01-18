using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.U2D;

public class ObjectPooling : SerializedMonoBehaviour
{
    public static ObjectPooling Instance = null;
    public static UnityEvent<IPool> OnObjectPooled = new UnityEvent<IPool>();
    [SerializeField] private SpriteAtlas spriteAtlasTarotCard;
    public SpriteAtlas SpriteAtlasTarotCard
    {
        get { return spriteAtlasTarotCard; }
    }
    [SerializeField] private SpriteAtlas spriteAtlasItems;
    public SpriteAtlas SpriteAtlasItems
    {
        get { return spriteAtlasItems; }
    }
    [SerializeField] private List<EffectDestroyObject> EffectDestroyObj = new List<EffectDestroyObject>();
    [SerializeField] private List<ObjDropHeart> ObjDropHeart = new List<ObjDropHeart>();
    [SerializeField] private List<UIHeart> heartObj = new List<UIHeart>();

    [SerializeField] private List<ObjectDropOnWorld> objectDropOnWorld = new List<ObjectDropOnWorld>();
    [SerializeField] private List<Enemy> eneScamps = new List<Enemy>();
    [SerializeField] private List<ImpactableObjects> impactableObjects = new List<ImpactableObjects>();
    [SerializeField] private List<ChestBonus> chestBonus = new List<ChestBonus>();
    [SerializeField] private List<ObjectSkill> objectSkill = new List<ObjectSkill>();
    [SerializeField] private List<ObjectSkill> objectSkillEnemy = new List<ObjectSkill>();
    [SerializeField] private List<ObjEffectAnimation> objEffectAnimation = new List<ObjEffectAnimation>();
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    public EffectDestroyObject PopChestEffectDestroyObj(string name, bool show = false)
    {
        return PopObjectFormPool<EffectDestroyObject>(EffectDestroyObj, name, GameConstants.Effect, show);
    }
    public void PushToPoolEffectDestroyObj(EffectDestroyObject chest)
    {
        PushToPool(chest, EffectDestroyObj);
    }
    public ObjEffectAnimation PopObjEffectAnimation(string name, bool show = false)
    {
        return PopObjectFormPool<ObjEffectAnimation>(objEffectAnimation, name, GameConstants.EffectAnimation, show);
    }
    public void PushToPoolObjEffectAnimation(ObjEffectAnimation chest)
    {
        PushToPool(chest, objEffectAnimation);
    }



    public ObjectSkill PopChestObjectSkill(string name, bool show = false)
    {
        return PopObjectFormPool<ObjectSkill>(objectSkill, name, GameConstants.EffectSkill, show);
    }
    public void PushToPoolObjectSkill(ObjectSkill chest)
    {
        PushToPool(chest, objectSkill);
    }

    public ObjectSkill PopChestObjectSkillEnemy(string name, bool show = false)
    {
        return PopObjectFormPool<ObjectSkill>(objectSkillEnemy, name, GameConstants.EffectEnemySkill, show);
    }
    public void PushToPoolObjectSkillEnemy(ObjectSkill chest)
    {
        PushToPool(chest, objectSkillEnemy);
    }

    public ChestBonus PopChestBonus(string name, bool show = false)
    {
        return PopObjectFormPool<ChestBonus>(chestBonus, name, GameConstants.ChestBonus, show);
    }
    public void PushToPoolChestBonus(ChestBonus chest)
    {
        PushToPool(chest, chestBonus);
    }

    public Enemy PopEnemy(string name,bool show = false)
    {
        return PopObjectFormPool<Enemy>(eneScamps, name, GameConstants.Enemy, show);
    }
    public void PushToPoolEnemy(Enemy ene)
    {
        PushToPool(ene, eneScamps);
    }


    public ObjectDropOnWorld PopObjectDrop(string name, bool show = false)
    {
        return PopObjectFormPool<ObjectDropOnWorld>(objectDropOnWorld, name, GameConstants.Object, show);
    }
    public void PushToPoolObjectDrop(ObjectDropOnWorld ObjectDrop)
    {
        PushToPool(ObjectDrop, objectDropOnWorld);
    }


    public ImpactableObjects PopImpactableObjects(string name, bool show = false)
    {
        return PopObjectFormPool<ImpactableObjects>(impactableObjects, name, GameConstants.EnemyDead, show);
    }
    public void PushToPoolImpactableObjects(ImpactableObjects impactableObject)
    {
        PushToPool(impactableObject, impactableObjects);
    }




    public ObjDropHeart PopDropHeart(bool show = false)
    {
        return PopObjectFormPool<ObjDropHeart>(ObjDropHeart, "ObjDropHeart", GameConstants.Object, show);
    }

    public UIHeart PopUIpHeart(bool show = false)
    {
        return PopObjectFormPool<UIHeart>(heartObj, "UIHeart", GameConstants.UIObject, show);
    }

    public void PushToPoolHeart(UIHeart uiheart)
    {
        PushToPool(uiheart, heartObj);
    }
    public T PopObjectFormPool<T>(List<T> pool, string Name, string path, bool show) where T : MonoBehaviour, IPool, new()
    {
        return PopFromPool(pool, Name, path, show);
    }
    private T PopFromPool<T>(List<T> pool, string objectName, string path, bool show) where T : MonoBehaviour, IPool, new()
    {
        // Logic để lấy 1 vật thể từ pool ra

        T obj = pool.Find(e => e.objectName.Equals(objectName));
        if (obj == null)
        {
            GameObject objAsset = Addressables.LoadAssetAsync<GameObject>(string.Format(path, objectName)).WaitForCompletion();
            objAsset.name = objectName;
            GameObject newObj = Instantiate(objAsset, transform);
            T value = newObj.GetComponent<T>();
            if (show)
                value.Show();
            pool.Remove(obj);
            return value;
        }
        if (show)
            obj.Show();
        pool.Remove(obj);
        return obj;
    }
    private void PushToPool<T>(T objectToPush, List<T> pool) where T : MonoBehaviour, IPool, new()
    {
        if (pool.Contains(objectToPush))
            return;
        objectToPush.transform.SetParent(transform, true);
        pool.Add(objectToPush);
    }

}
