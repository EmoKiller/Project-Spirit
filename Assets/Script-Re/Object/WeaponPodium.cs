using UnityEngine;

public class WeaponPodium : TriggerWaitAction
{
    [SerializeField] Weapon weapon = null;
    float damagePlayer = 0;
    float speedPlayer = 0;
    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        GetDamagePlayer(other);
        base.OnTriggerEnter(other);
        ShowInfoWeaponPodium();
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        EventDispatcher.Publish(InfoWeapon.Script.InfoWeapon, Events.SetDefault);
    }
    protected override void OnTringgerWaitAction()
    {
        if (actioned)
            return;
        EventDispatcher.Publish(InfoWeapon.Script.InfoWeapon, Events.SetDefault);
        EventDispatcher.Publish(Player.Script.Player, Events.PlayerChangeWeapon, weapon);
        weapon.gameObject.SetActive(false);
        enabled = false;
        base.OnTringgerWaitAction();
    }
    private void GetDamagePlayer(Collider other)
    {
        CharacterAttack charatk = other.GetComponent<CharacterAttack>();
        damagePlayer = charatk.TotalDamage();
        speedPlayer = charatk.Speed;
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
}
