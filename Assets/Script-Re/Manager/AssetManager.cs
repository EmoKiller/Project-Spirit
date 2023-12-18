using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.U2D;

public class AssetManager : SerializedMonoBehaviour
{
    public static AssetManager Instance = null;
    public Dictionary<ListTypeEffects, GameObject> ListEffect = new Dictionary<ListTypeEffects, GameObject>();
    public Dictionary<TypeEnemy, GameObject> ListEnemy = new Dictionary<TypeEnemy, GameObject>();
    public Dictionary<ListDropItems, GameObject> DropItems = new Dictionary<ListDropItems, GameObject>();
    public SpriteAtlas SpriteAtlasItems;
    public GameObject HeartObj;
    public GameObject ObjDropExp;
    public GameObject ObjDropCoins;
    public GameObject ObjDropAngry;
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
    //public void InstantiateObj2()
    //{
    //    ItemDropObject = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Object/ItemDropObject.prefab").WaitForCompletion();

    //}
    public void InstantiateItems(string str, Transform pos, Vector3 dir)
    {
        GameObject item = null;
        Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
        {
            item = handle.Result;
            GameObject obj = Instantiate(item, pos);
            obj.transform.position = dir + new Vector3(0, 1f, 0);
        };
    }
    public Sprite Instantia()
    {
        Sprite item = null;
        Addressables.LoadAssetAsync<Sprite>("Assets/Prefabs/UI/Hearts/Red/Empty.asset").Completed += (handle) =>
        {
            item = handle.Result;
        };
        return item;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
