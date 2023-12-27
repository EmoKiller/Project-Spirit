﻿using Sirenix.OdinInspector;
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

    [SerializeField]private SpriteAtlas spriteAtlasTarotCard;
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
    public List<UIHeart> HeartObj
    {
        get { return heartObj;}
    }
    [SerializeField] private List<ObjDropExp> ObjDropExp = new List<ObjDropExp>();
    [SerializeField] private List<ObjDropCoin> ObjDropCoins = new List<ObjDropCoin>();
    [SerializeField] private List<ObjDropAngry> ObjDropAngry = new List<ObjDropAngry>();
    [SerializeField] private List<ObjDropTarotCard> ObjDropTarotCard = new List<ObjDropTarotCard>();
    [SerializeField] private Dictionary<ChestType, ChestBonus> ObjectChestBonus = new Dictionary<ChestType, ChestBonus>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public ObjDropHeart PopDropHeart(bool show = false)
    {
        return PopObjectFormPool<ObjDropHeart>(ObjDropHeart, "ObjDropHeart", GameConstants.Object, show);
    }
    public ObjDropExp PopObjDropExp(bool show = false)
    {
        return PopObjectFormPool<ObjDropExp>(ObjDropExp, "ObjDropExp", GameConstants.Object, show);
    }
    public ObjDropCoin PopObjDropCoins(bool show = false)
    {
        return PopObjectFormPool<ObjDropCoin>(ObjDropCoins, "ObjDropCoin", GameConstants.Object, show);
    }
    public ObjDropAngry PopObjDropAngry(bool show = false)
    {
        return PopObjectFormPool<ObjDropAngry>(ObjDropAngry, "ObjDropAngry", GameConstants.Object, show);
    }
    public ObjDropTarotCard PopObjDropTarotCard(bool show = false)
    {
        return PopObjectFormPool<ObjDropTarotCard>(ObjDropTarotCard, "ObjDropTarotCard", GameConstants.Object, show);
    }
    public void PushToPoolDropTarotCard(ObjDropTarotCard DropTarotCard)
    {
        PushToPool(DropTarotCard, ObjDropTarotCard);
    }
    public UIHeart PopUIpHeart(bool show = false)
    {
        return PopObjectFormPool<UIHeart>(HeartObj, "UIHeart", GameConstants.UIObject, show);
    }
    public void PushToPoolHeart(UIHeart uiheart)
    {
        PushToPool(uiheart, HeartObj);
    }

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
        if (show)
            obj.Show();
        return obj;
    }
    private void PushToPool<T>(T objectToPush, List<T> pool) where T : MonoBehaviour, IPool, new()
    {
        objectToPush.transform.SetParent(transform,true);
        pool.Add(objectToPush);
    }

}
