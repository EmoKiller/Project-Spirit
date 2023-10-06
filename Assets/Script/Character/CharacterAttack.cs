using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Weapon currentWeapon = null;
    public float AttackRange => currentWeapon.weaponObject.attackRange;

    private void Awake()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    public void Initialized()
    {

    }

    public void Attack(Vector3 target)
    {
        currentWeapon.Attack(target);
    }

}
