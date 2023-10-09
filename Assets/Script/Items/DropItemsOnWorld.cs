using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.XR;

public class DropItemsOnWorld : MonoBehaviour
{
    public ItemsType type;
    public GameObject Item = null;
    //public Image showButton = null;
    private void Awake()
    {
        //showButton.gameObject.SetActive(false);
    }
    void Start()
    {
        GameManager.Instance.assetManager.InstantiateSword(GameManager.Instance.assetManager.Weapon, transform);
    }

    
}
