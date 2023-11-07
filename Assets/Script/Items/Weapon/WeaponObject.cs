using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "GameUtilities/CreateWeapon")]
public class WeaponObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;

    [Header("Comfinguration")]
    public float AttackRange = 2f;
    public float PowerForce = 2f;
    public float weight = 1f;
    public List<float> ListDamage = new List<float>();
    public WeaponObject()
    {

    }
}
