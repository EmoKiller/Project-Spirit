using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]private Weapon currentWeapon = null;
    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float PowerForce => currentWeapon.weaponObject.PowerForce;
    public float Weight => currentWeapon.weaponObject.weight;
    public List<float> CurrentHit => currentWeapon.weaponObject.ListDamage;
    public void Initialized()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        
    }
    public void Attack(Vector3 target)
    {
        currentWeapon.Attack(target);
    }
    

}
