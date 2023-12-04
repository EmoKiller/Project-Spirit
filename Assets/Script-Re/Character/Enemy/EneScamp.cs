public class EneScamp : Enemy
{

    protected override void Start()
    {
        base.Start();
        Init();
    }
    private void Init()
    {
        maxHealth = characterAttack.HP;
        health = maxHealth;
        healthBar.SetHealh(maxHealth);
        SetoffSlash();
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        slash.AddActionAttack(OnAttackHit);
        //deadAction = Dead;
        deadBody.SetActive(false);
    }
    
}
