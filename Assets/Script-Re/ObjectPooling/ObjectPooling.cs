using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPooling : SerializedMonoBehaviour
{
    public static ObjectPooling Instance = null;
    public static UnityEvent<IPool> OnObjectPooled = new UnityEvent<IPool>();
    public List<EffectDestroyObject> EffectDestroyObj = new List<EffectDestroyObject>();
    public List<ObjDropHeart> ObjDropHeart = new List<ObjDropHeart>();
    public List<UIHeart> HeartObj = new List<UIHeart>();
    public List<ObjDropExp> ObjDropExp = new List<ObjDropExp>();
    public List<ObjDropCoin> ObjDropCoins = new List<ObjDropCoin>();
    public List<ObjDropAngry> ObjDropAngry = new List<ObjDropAngry>();
    public List<IPool> pools = new List<IPool>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    private void Start()
    {
        PoolInstantiateDictionaryObj(EffectDestroyObj, AssetManager.Instance.ListEffect, transform);
        PoolInstantiateObj(ObjDropHeart, AssetManager.Instance.DropItems[ListDropItems.Heart], transform);
        PoolInstantiateObj(HeartObj, AssetManager.Instance.HeartObj, transform, 15);
        PoolInstantiateObj(ObjDropExp, AssetManager.Instance.ObjDropExp, transform, 5);
        PoolInstantiateObj(ObjDropCoins, AssetManager.Instance.ObjDropCoins, transform, 10);
        PoolInstantiateObj(ObjDropAngry, AssetManager.Instance.ObjDropAngry, transform, 10);
    }
    public void PoolInstantiateObj<T>(List<T> pool,GameObject gameObject,Transform tranform)
    {
        GameObject obj = Instantiate(gameObject, tranform);
        T scr = obj.GetComponent<T>();
        pool.Add(scr);
    }
    public void PoolInstantiateObj<T>(List<T> pool, GameObject gameObject, Transform tranform,int Quantity)
    {
        for (int i = 0; i < Quantity; i++)
        {
            GameObject obj = Instantiate(gameObject, tranform);
            T scr = obj.GetComponent<T>();
            pool.Add(scr);
        }
    }
    public void PoolInstantiateDictionaryObj<T,T2>(List<T> pool, Dictionary<T2, GameObject> listGameObject, Transform tranform) where T2 : Enum
    {
        foreach (var item in listGameObject)
        {
            GameObject obj = Instantiate(item.Value, tranform);
            T scr = obj.GetComponent<T>();
            pool.Add(scr);
        }
    }
    public T PopObjectFormPool<T>(List<T> pool,string Name) where T : MonoBehaviour, IPool, new()
    {
        return PopFromPool(Name, pool);
    }
    private T PopFromPool<T>(string objectName, List<T> pool) where T : MonoBehaviour, IPool, new()
    {
        // Logic để lấy 1 vật thể từ pool ra
        T obj = pool.Find(e => e.objectName.Equals(objectName) && e.isActiveAndEnabled == false);
        if (obj == null)
        {
            T obj2 = pool.Find(e => e.objectName.Equals(objectName));
            T obj3 = Instantiate(obj2, transform);
            pool.Add(obj3);
            return obj3;
        }
        return obj;
    }

    //public T PushToPool<T>(T objectToPush, List<T> pool) where T : MonoBehaviour, IPool, new()
    //{

    //    return 
    //}
}
