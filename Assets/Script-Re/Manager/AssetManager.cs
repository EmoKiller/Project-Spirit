using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

public class AssetManager : SerializedMonoBehaviour
{
    public static AssetManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }
    private void Start()
    {
       
    }
    //public void InstantiateObj()
    //{
    //    Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Object/ItemDropObject.prefab").Completed += (handle) =>
    //    {
    //        ItemDropObject = handle.Result;
    //    };
    //}
    public GameObject Instantiate(string str)
    {
        GameObject obj = Addressables.LoadAssetAsync<GameObject>(str).WaitForCompletion();
        return obj;
    }
        
}

    //public void Instantiate(string str, Transform pos, Vector3 dir)
    //{
    //    GameObject item = null;
    //    Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
    //    {
    //        item = handle.Result;
    //        GameObject obj = Instantiate(item, pos);
    //    };
    //}
    //public Sprite Instantia()
    //{
    //    Sprite item = null;
    //    Addressables.LoadAssetAsync<Sprite>("Assets/Prefabs/UI/Hearts/Red/Empty.asset").Completed += (handle) =>
    //    {
    //        item = handle.Result;
    //    };
    //    return item;
    //}

