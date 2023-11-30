using UnityEngine;

public class WeaponPodium : TriggerWaitAction
{
    [SerializeField] Weapon weapon = null;
    float damagePlayer = 0;
    float speedPlayer = 0;
    float num = 0;
    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }
    
    private void UpdateValue(float value)
    {
        num = value;
    }
    private void GetDamagePlayer(Collider other)
    {
        CharacterAttack charatk = other.GetComponent<CharacterAttack>();
        damagePlayer = charatk.TotalDamage();
        speedPlayer = charatk.Speed;
    }
    private float CompareDamage()
    {
        float dmg = weapon.weaponObject.TotalDamage() - damagePlayer;
        return dmg;
    }
    private float CompareSpeed()
    {
        float speed = weapon.weaponObject.Speed - speedPlayer;
        return speed;
    }
    private void ShowInfoWeaponPodium()
    {
        EventDispatcher.Publish(InfoWeapon.Script.InfoWeapon, Events.UpdateInfoWeapon,
            weapon.weaponObject.NameWeapon,
            weapon.weaponObject.QuoteWeapon,
            weapon.weaponObject.DescriptionWeapon,
            CompareDamage(),
            CompareSpeed()
            );
    }
}
