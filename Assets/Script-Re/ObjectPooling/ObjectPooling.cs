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
    public SpriteAtlas spriteAtlasTarotCard;
    public List<EffectDestroyObject> EffectDestroyObj = new List<EffectDestroyObject>();
    public List<ObjDropHeart> ObjDropHeart = new List<ObjDropHeart>();
    public List<UIHeart> HeartObj = new List<UIHeart>();
    public List<ObjDropExp> ObjDropExp = new List<ObjDropExp>();
    public List<ObjDropCoin> ObjDropCoins = new List<ObjDropCoin>();
    public List<ObjDropAngry> ObjDropAngry = new List<ObjDropAngry>();
    public Dictionary<ChestType, ChestBonus> ObjectChestBonus = new Dictionary<ChestType, ChestBonus>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    private void Start()
    {
        
    }

    public ObjDropHeart PopDropHeart(string objName, bool show = false)
    {
        return PopObjectFormPool<ObjDropHeart>(ObjDropHeart, objName, GameConstants.Object, show);
    }
    public UIHeart PopUIpHeart(string objName, bool show = false)
    {
        return PopObjectFormPool<UIHeart>(HeartObj, objName, GameConstants.UIObject, show);
    }

    //public void PoolInstantiateObj<T>(List<T> pool, GameObject gameObject, Transform tranform,int Quantity)
    //{
    //    for (int i = 0; i < Quantity; i++)
    //    {
    //        GameObject obj = Instantiate(gameObject, tranform);
    //        T scr = obj.GetComponent<T>();
    //        pool.Add(scr);
    //    }
    //}
    //public void PoolInstantiateDictionaryObj<T,T2>(List<T> pool, Dictionary<T2, GameObject> listGameObject, Transform tranform) where T2 : Enum
    //{
    //    foreach (var item in listGameObject)
    //    {
    //        GameObject obj = Instantiate(item.Value, tranform);
    //        T scr = obj.GetComponent<T>();
    //        pool.Add(scr);
    //    }
    //}
    public T PopObjectFormPool<T>(List<T> pool,string Name, string path, bool show) where T : MonoBehaviour, IPool, new()
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
            GameObject newObj = Instantiate(objAsset, transform);
            T value = newObj.GetComponent<T>();
            if (show)
                value.Show();
            return value;
        }
        return obj;
    }
    public void PushToPool<T>(T objectToPush, List<T> pool) where T : MonoBehaviour, IPool, new()
    {
        objectToPush.transform.SetParent(transform,true);
        pool.Add(objectToPush);
    }
}
