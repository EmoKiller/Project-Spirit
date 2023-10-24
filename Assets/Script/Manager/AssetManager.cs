using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetManager : MonoBehaviour
{
    public string Weapon;
    public string ShowHPEnemy;
    public string Enemy;
    public string SlashHit;
    //public void SpawnItems(Vector3 position, Quaternion quaternion)
    //{
    //    Weapon.InstantiateAsync(position, quaternion);
    //}
    public void InstantiateItems(string str, Transform pos)
    {
        //GameObject weaponObj = Resources.Load<GameObject>(string.Format(GameConstants.Sword, "AdvancedShortSword"));
        //Instantiate();
        GameObject item = null;
        //Addressables.InstantiateAsync("Assets/Prefabs/Weapon/Sword/AdvancedShortSword.prefab").Completed += (handle) => 
        //{
        //    handle.Result
        //    //Instantiate(weapon, hand);
        //};

        Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
        {
            item = handle.Result;
            Instantiate(item, pos);
        };
    }
}
