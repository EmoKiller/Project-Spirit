using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]private Weapon currentWeapon = null;
    public float AttackRange => currentWeapon.weaponObject.attackRange;
    public float Damage => currentWeapon.weaponObject.damage;


    private void Awake()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }
    public void Initialized()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    public void Attack(Vector3 target)
    {
        currentWeapon.Attack(target);
    }

}
