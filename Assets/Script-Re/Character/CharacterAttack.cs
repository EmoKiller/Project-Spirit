using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon = null;
    [SerializeField] private CursesEquip currentCurses = null;
    [SerializeField] private HPObject hpObject = null;
    public LevelItems LevelWeapon => currentWeapon.weaponObject.LevelWeapon;
    public string NameWeapon => currentWeapon.weaponObject.NameWeapon;
    public string QuoteWeapon => currentWeapon.weaponObject.QuoteWeapon;
    public string DescriptionWeapon => currentWeapon.weaponObject.DescriptionWeapon;
    public float Speed => currentWeapon.weaponObject.Speed;
    public float AttackRange => currentWeapon.weaponObject.AttackRange;
    public float HP => hpObject.HP;
    public float PowerForce => hpObject.PowerForce;
    public float Weight => hpObject.weight;
    public List<float> CurrentHit => currentWeapon.weaponObject.ListDamage;
    public Vector3 SlashBoxSize => currentWeapon.weaponObject.SlashBoxSize;
    public void Initialized()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }
    public void Initialized(Weapon weapon)
    {
        currentWeapon = weapon;
        //EventDispatcher.Publish(UiDungeonManager.Script.UiDungeonManager, Events.UpdateIconWeapon, currentWeapon.weaponObject.IconWeapon);
    }
    public void Initialized(CursesEquip curses)
    {
        currentCurses = curses;

    }
    public bool BoolWeaponEquip()
    {
        return currentWeapon is not null;
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
