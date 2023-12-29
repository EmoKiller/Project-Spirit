using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewHP", menuName = "GameUtilities/CreateHP")]
public class HPObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;
    [Header("Confinguretion")]
    public float HP = 1;
    public float PowerForce = 30f;
    public float weight = 30f;
    public float ExpDrop = 1;

    public HPObject() 
    { 

    }
}
