using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "GameUtilities/CreateWeapon")]
public class WeaponObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;

    [Header("Comfinguration")]
    public Sprite IconWeapon;
    public LevelRomanNumerals LevelWeapon;
    public string NameWeapon = "";
    public string QuoteWeapon = "";
    public string DescriptionWeapon = "";
    public float Speed = 1;
    public float AttackRange = 2f;
    public float AttackRangeBow = 2f;
    public float RangeDash = 4;
    public float SpeedOnDash = 16;
    public float NomalSpeed = 4;
    public Vector3 SlashBoxSize = new Vector3();
    public List<float> ListDamage = new List<float>();
    public List<float> ListPowerForce = new List<float>();
    public List<float> MoveOnAttack = new List<float>();
    public WeaponObject()
    {
    }
    
}
