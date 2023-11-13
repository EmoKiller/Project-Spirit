using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
    //public void SpawnItems(Vector3 position, Quaternion quaternion)
    //{
    //    Weapon.InstantiateAsync(position, quaternion);
    //}
    public void InstantiateItems(string str, Transform pos , Vector3 dir)
    {
        //GameObject weaponObj = Resources.Load<GameObject>(string.Format(GameConstants.Sword, "AdvancedShortSword"));
        //Instantiate();
        GameObject item = null;
        //Addressables.InstantiateAsync("Assets/Prefabs/Weapon/Sword/AdvancedShortSword.prefab").Completed += (handle) => 
        //{
        //    handle.Result
        //    //Instantiate(weapon, hand);
        //};
        
        //Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
        //{
        //    item = handle.Result;
        //    GameObject obj = Instantiate(item, pos);
        //    obj.transform.position = dir + new Vector3(0,1f,0);
        //};
        Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
        {
            item = handle.Result;
            GameObject obj = Instantiate(item, pos);
            obj.transform.position = dir + new Vector3(0, 1f, 0);
        };
    }
    public Sprite Instantia()
    {
        //Debug.Log("Assets/Prefabs/UI/Heart/Size/0-Heart_Nomal.prefab");
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
