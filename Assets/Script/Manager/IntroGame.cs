using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGame : MonoBehaviour
{
    [SerializeField] GameObject map1;
    [SerializeField] GameObject map2;



    public void SetOnMap(GameObject map, bool value )
    {
        map.SetActive(value);
    }
}
