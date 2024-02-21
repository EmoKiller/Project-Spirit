using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EneGuardian : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        characterAnimator.AddSpawnObj(SpawnChain, SpawnObjBallJumpRandom, SpawnObj3, SpawnObj4);
        characterAnimator.AddTriggerSound(TriggerSound, TriggerSound2);
    }
    public override void Init()
    {
        base.Init();
        ObseverConstants.OnSpawnBoss?.Invoke();
        healthBar.SetHealthSlider(UIManager.Instance.HealthBoss.HealthSlider);
        healthBar.SetHealh(maxHealth);
        healthBar.UpdateHealth(maxHealth);
        Debug.Log("setHealthBarBoss");
    }
    protected override void Update()
    {
        if (OnAction || OnEvent || !Alive)
        {
            return;
        }
        if (onAniATK)
            return;
        if (Distance() > playerDetectionRange && !enemyRunFollow)
        {
            randomMove = false;
            EnemyThinking(1, 10);
            return;
        }
        if (Distance() <= playerDetectionRange - 1 && Distance() <= playerDetectionRange + 1 && randomMove)
        {
            RandomMove();
            return;
        }
        if (onFollowPlayer && Distance() > characterAttack.AttackRange && !randomMove)
        {
            randomMove = false;
            MoveTo(direction.transform.position);
            Rotation();
            return;
        }
        if (onFollowPlayer && Distance() <= characterAttack.AttackRange)
        {
            Rotation();
            characterAnimator.SetTrigger("Attack");
            return;
        }
    }
    private void EnemyThinking(float TimeThink, int ratioRandomMove)
    {
        if (enemyThinking || !Alive)
            return;
        enemyThinking = true;
        onFollowPlayer = false;
        enemyRunFollow = false;
        this.DelayCall(TimeThink, () =>
        {
            int i = Random.Range(0, 100);
            enemyThinking = false;
            if (i < ratioRandomMove)
            {
                IsRandomMove();
                characterAnimator.SetTrigger("Move");
                return;
            }
            IsFollowAtK();
        });
    }
    private void RandomMove()
    {
        if (!randomMove)
            return;
        onFollowPlayer = false;
        randomMove = false;
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 15 + direction.transform.position;
        SetMoveWayPoint(point, 3);
        EnemyThinking(3, 50);
    }
    protected void IsFollowAtK()
    {
        randomMove = false;
        onFollowPlayer = true;
        int i = Random.Range(0, 100);
        if (i < 55)
        {
            characterAnimator.SetTrigger("RunFollow");
            agent.moveSpeed = 8f;
            enemyRunFollow = true;
            onFollowPlayer = true;
            return;
        }
        OnUseSKill();
    }
    protected void OnUseSKill()
    {
        if (Distance() <= characterAttack.AttackRangeBow)
        {
            int i = Random.Range(0, 100);
            if (i < 35)
            {
                characterAnimator.SetTrigger("UseSKill1");
                return;
            }
            if (i > 70)
            {
                characterAnimator.SetTrigger("UseSKill2");
                return;
            }
            characterAnimator.SetTrigger("UseSKill3");
            return;
        }
        EnemyThinking(0.5f, 100);
    }
    public override void TakeDamage(float damage)
    {
        if (!Alive)
            return;
        RewardSystem.Instance.SpawnObjEffectAnimation(TypeEffectAnimation.FxAniSlash, transform.position + new Vector3(0, 2, 0));
        CurrentHealth -= damage;
        healthBar.UpdateHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Dead();
            ObseverConstants.OnBossDeath?.Invoke();
        }
    }
    protected void SpawnObj3()
    {
        SpawnObjBallFireLoop(8);
    }
    protected void SpawnObj4()
    {
        Rotation();
        SpawnChain(direction.position);
    }
    protected void TriggerSound() => AudioManager.instance.Play("FireSkillEnemy");
    protected void TriggerSound2() => AudioManager.instance.Play("SlashEnemy");
    protected override void StartAniAtk()
    {
        Rotation();
        base.StartAniAtk();
    }
    protected override void FinishAniAtk()
    {
        characterAnimator.SetTrigger("Idie");
        base.FinishAniAtk();
        EnemyThinking(2f, 35);
    }
}
