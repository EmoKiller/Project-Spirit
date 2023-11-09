using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "GameUtilities/CreateWeapon")]
public class WeaponObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;

    [Header("Comfinguration")]
    public string NameWeapon = "";
    public string QuoteWeapon = "";
    public string DescriptionWeapon = "";
    public float Speed = 1;
    public Vector3 SizeSlashBox = new Vector3();
    public float AttackRange = 2f;
    public List<float> ListDamage = new List<float>();
    
    public WeaponObject()
    {

    }
    public float TotalDamage()
    {
        float a = 0;
        foreach (float Damage in ListDamage)
        {
            a += Damage;
        }
        return a;
    }
}
