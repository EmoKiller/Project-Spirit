using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPodium : OnTringgerWaitAction
{
    [SerializeField] Weapon weapon = null;
    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        //other.GetComponent<CharacterAttack>();
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.SetInfoWeapon, true);
        ShowInfoWeaponPodium();
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.SetInfoWeapon, false);
    }
    private void ShowInfoWeaponPodium()
    {
        EventDispatcher.Publish(ListScript.InfoWeapon, Events.UpdateValue,
            weapon.weaponObject.NameWeapon,
            weapon.weaponObject.QuoteWeapon,
            weapon.weaponObject.DescriptionWeapon,
            weapon.weaponObject.TotalDamage().ToString(),
            weapon.weaponObject.Speed.ToString()
            );
    }
    private void CompareWeapon()
    {
        
    }
}
