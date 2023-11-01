using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneScamp : Enemy
{

    protected override void Start()
    {
        base.Start();
        Init();
    }
    private void Init()
    {
        maxHealth = 100;
        health = maxHealth;
        healthBar.SetHealh(maxHealth);
        slash.SetSizeBox(4, 1, 4);
        SetoffSlash();
        characterAnimator.AddStepAni(StartAni, SetOnSlash, SetoffSlash, FinishAni);
        slash.AddActionAttack(OnAttackHit);
        deadAction = Dead;
    }

}
