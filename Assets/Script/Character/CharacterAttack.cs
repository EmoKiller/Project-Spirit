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

    public float Damage = 0;
    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float HP => HPObject.HP;
    public float PowerForce => HPObject.PowerForce;
    public float Weight => HPObject.weight;
    public List<float> CurrentHit => currentWeapon.weaponObject.ListDamage;
    public void Initialized()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        Damage = currentWeapon.weaponObject.TotalDamage();
    }
    public void Attack(Vector3 target)
    {
        currentWeapon.Attack(target);
    }
    

}
