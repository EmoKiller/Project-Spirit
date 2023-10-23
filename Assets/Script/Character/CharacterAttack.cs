using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]private Weapon currentWeapon = null;

    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float firsthit => currentWeapon.weaponObject.Firsthit;
    public float SecondHit => currentWeapon.weaponObject.SecondHit;
    public float ThirdHit => currentWeapon.weaponObject.ThirdHit;
    public float FourHit => currentWeapon.weaponObject.FourHit;



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
