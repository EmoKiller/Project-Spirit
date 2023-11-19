using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public string Weapon;
    public string ShowHPEnemy;
    public string Enemy;
    public string SlashHit;
    public string hearts;

    public Dictionary<Enemys,GameObject> weapons = null;

    public GameObject ObjDrop;
    public Dictionary<ChestType,GameObject> Chest;

    private void Start()
    {
        
    }
    public void InstantiateObj()
    {
        Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/ObjectDrop/ItemDrop.prefab").Completed += (handle) =>
        {
            ObjDrop = handle.Result;
        };
    }
    public void InstantiateItems(string str, Transform pos , Vector3 dir)
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
