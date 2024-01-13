using UnityEngine;

public class WeaponPodium : TriggerWaitAction 
{
    [SerializeField] Weapon weapon = null;
    [SerializeField] BoxCollider boxCollider = null;
    float damagePlayer = 0;
    float speedPlayer = 0;
    public void SetBoxCollider(bool value)
    {
        boxCollider.enabled = value;
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
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.SetDefault);
    }
    protected override void OnTringgerWaitAction()
    {
        if (actioned)
            return;
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.SetDefault);
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UpdateIconWeapon,weapon.weaponObject.IconWeapon);
        EventDispatcher.Publish(Player.Script.Player, Events.PlayerChangeWeapon, weapon);
        weapon.gameObject.SetActive(false);
        enabled = false;
        base.OnTringgerWaitAction();
    }
    private void GetDamagePlayer(Collider other)
    {
        CharacterAttack charatk = other.GetComponent<CharacterAttack>();
        if (charatk != null)
            return;
        damagePlayer = charatk.TotalDamage();
        speedPlayer = charatk.Speed;
    }
    private void ShowInfoWeaponPodium()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UpdateInfoWeapon,
            weapon.weaponObject.NameWeapon,
            weapon.weaponObject.QuoteWeapon,
            weapon.weaponObject.DescriptionWeapon,
            CompareDamage(),
            CompareSpeed()
            );
    }
    private float CompareDamage()
    {
        float dmg = weapon.TotalDamage() - damagePlayer;
        return dmg;
    }
    private float CompareSpeed()
    {
        float speed = weapon.weaponObject.Speed - speedPlayer;
        return speed;
    }
}
