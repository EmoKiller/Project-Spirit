using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.XR;

public class DropItemsOnWorld : MonoBehaviour
{
    public ItemsType type;
    public GameObject Item = null;
    public bool IsTake
    {
        get { return isTake; }
        set { isTake = value; }
    }
    private bool isTake;
    public void TakeItems()
    {
        IsTake = true;
    }

    
}
