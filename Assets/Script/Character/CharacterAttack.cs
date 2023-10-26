using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]private Weapon currentWeapon = null;
    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float PowerForce => currentWeapon.weaponObject.PowerForce;
    public float Weight => currentWeapon.weaponObject.weight;
    public List<float> CurrentHit = new List<float>();
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
        CurrentHit.Add(currentWeapon.weaponObject.FirstHit);
        CurrentHit.Add(currentWeapon.weaponObject.SecondHit);
        CurrentHit.Add(currentWeapon.weaponObject.ThirdHit);
        CurrentHit.Add(currentWeapon.weaponObject.FourHit);
    }

}
