using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon = null;
    [SerializeField] private HPObject HPObject = null;
    public string NameWeapon => currentWeapon.weaponObject.NameWeapon;
    public string QuoteWeapon => currentWeapon.weaponObject.QuoteWeapon;
    public string DescriptionWeapon => currentWeapon.weaponObject.DescriptionWeapon;
    public float Speed => currentWeapon.weaponObject.Speed;
    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float HP => HPObject.HP;
    public float PowerForce => HPObject.PowerForce;
    public float Weight => HPObject.weight;
    public List<float> CurrentHit => currentWeapon.weaponObject.ListDamage;
    public void Initialized()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }
    public void Initialized(Weapon weapon)
    {
        currentWeapon = weapon;
    }
    public void Attack(Vector3 target)
    {
        currentWeapon.Attack(target);
    }
    public float TotalDamage()
    {
        return currentWeapon.weaponObject.TotalDamage();
    }
    

}
