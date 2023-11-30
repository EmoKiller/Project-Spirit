using UnityEngine;

public class WeaponPodium : OnTringgerWaitAction
{
    [SerializeField] Weapon weapon = null;
    float damagePlayer = 0;
    float speedPlayer = 0;
    float num = 0;
    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (actioned)
            return;
        GetDamagePlayer(other);
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.SetInfoWeapon, true);
        EventDispatcher.Addlistener<float>(ListScript.TypeButton, Events.UpdateValue, UpdateValue);
        ShowInfoWeaponPodium();
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.SetInfoWeapon, false);
        EventDispatcher.Publish(ListScript.InfoWeapon, Events.SetDefault);
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.RemoveEvent);
    }
    protected override void OnTringgerPlayer()
    {
        base.OnTringgerPlayer();
        if (actioned)
        {
            return;
        }
        if (num >= 1)
        {
            EventDispatcher.Publish(ListScript.Player,Events.ChangeWeapon,weapon);
            EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.SetInfoWeapon, false);
            EventDispatcher.Publish(ListScript.UIButtonAction, Events.SetDefaultButton);
            weapon.gameObject.SetActive(false);
            actioned = true;

        }
            
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
        EventDispatcher.Publish(ListScript.InfoWeapon, Events.UpdateValue,
            weapon.weaponObject.NameWeapon,
            weapon.weaponObject.QuoteWeapon,
            weapon.weaponObject.DescriptionWeapon,
            CompareDamage(),
            CompareSpeed()
            );
    }
}
