using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetManager : MonoBehaviour
{
    public string Weapon;
    //public void SpawnItems(Vector3 position, Quaternion quaternion)
    //{
    //    Weapon.InstantiateAsync(position, quaternion);
    //}
    public void InstantiateSword(string str, Transform hand)
    {
        //GameObject weaponObj = Resources.Load<GameObject>(string.Format(GameConstants.Sword, "AdvancedShortSword"));
        //Instantiate();
        GameObject weapon = null;
        //Addressables.InstantiateAsync("Assets/Prefabs/Weapon/Sword/AdvancedShortSword.prefab").Completed += (handle) => 
        //{
        //    handle.Result
        //    //Instantiate(weapon, hand);
        //};

        Addressables.LoadAssetAsync<GameObject>(str).Completed += (handle) =>
        {
            weapon = handle.Result;
            Instantiate(weapon, hand);
        };
    }
}
