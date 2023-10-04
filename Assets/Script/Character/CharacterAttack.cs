using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Weapon currentWeapon = null;
    public float AttackRange => currentWeapon.weaponObject.attackRange;

    public void Initialized()
    {
        
    }
}
