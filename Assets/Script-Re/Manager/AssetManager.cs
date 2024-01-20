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
    public GameObject Instantiate(string str)
    {
        GameObject obj = Addressables.LoadAssetAsync<GameObject>(str).WaitForCompletion();
        return obj;
    }
        
}


