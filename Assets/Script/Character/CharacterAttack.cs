using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]private Weapon currentWeapon = null;

    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float PowerForce => currentWeapon.weaponObject.PowerForce;
    public float Weight => currentWeapon.weaponObject.weight;
    public float firsthit => currentWeapon.weaponObject.FirstHit;
    public float SecondHit => currentWeapon.weaponObject.SecondHit;
    public float ThirdHit => currentWeapon.weaponObject.ThirdHit;
    public float FourHit => currentWeapon.weaponObject.FourHit;

    public List<float> CurrentHit = new List<float>();

    private void Awake()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        AddListDmg();
    }
    public void Initialized()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        AddListDmg();
    }

    public void Attack(Vector3 target)
    {
        currentWeapon.Attack(target);
    }
    private void AddListDmg()
    {
        CurrentHit.Add(firsthit);
        CurrentHit.Add(SecondHit);
        CurrentHit.Add(ThirdHit);
        CurrentHit.Add(FourHit);
    }

}
