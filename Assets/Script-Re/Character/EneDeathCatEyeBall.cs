using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EneDeathCatEyeBall : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        characterAnimator.AddSpawnObj(SpawnObjFireBalls);
    }
    protected override void Update()
    {
        if (OnAction || OnEvent || !Alive)
        {
            return;
        }
        if (onAniATK || enemyThinking)
            return;
        if (Distance() > playerDetectionRange && !onFollowPlayer)
        {
            EnemyThinking(1, 0, null, () => { onFollowPlayer = true; onTargetPlayer = false; });
            return;
        }
        if (Distance() < playerDetectionRange && onFollowPlayer && !randomMove && !onTargetPlayer)
        {
            randomMove = true;
            onTargetPlayer = true;
            onFollowPlayer = false;
            return;
        }
        if (onFollowPlayer && !randomMove && Distance() > playerDetectionRange)
        {
            MoveTo(direction.transform.position);
            Rotation();
            onTargetPlayer = false;
            return;
        }
        if (onTargetPlayer && randomMove && Distance() <= playerDetectionRange)
        {
            RandomMove();
            return;
        }
    }
    private void RandomMove()
    {
        if (!randomMove)
            return;
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 15 + direction.transform.position;
        randomMove = false;
        OnAction = true;
        onFollowPlayer = false;
        SetMoveWayPoint(point, 4);
        int i = Random.Range(0, 100);
        if (i < 70)
        {
            this.DelayCall(5, () =>
            {
                OnAttack();
            });
            return;
        }
        EnemyThinking(5, 100, () => { IsRandomMove(); }, null);
    }
    public override void TakeDamage(float damage)
    {
        if (!Alive)
            return;
        CurrentHealth -= damage;
        healthBar.SetActive();
        healthBar.UpdateHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }
    public override void Dead()
    {
        base.Dead();
    }
    protected void OnAttack()
    {
        if (Distance() <= characterAttack.AttackRangeBow)
        {
            characterAnimator.SetTrigger("UseSkill");
            return;
        }
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    protected void SpawnObjFireBalls()
    {
        int i = Random.Range(0, 100);
        if (i < 60)
        {
            SpawnObjBallFireLoop(8);
            return;
        }
        if (i > 80)
        {
            SpawnObjBallFire(8);
            return;
        }
        SpawnObjBallFire();
        return;
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        randomMove = false;
        if (Distance() > playerDetectionRange)
        {
            EnemyThinking(1, 0, null, () => { onFollowPlayer = true; });
            return;
        }
        EnemyThinking(2, 30, () => { IsRandomMove(); }, () => { OnAttack(); });
    }
}
