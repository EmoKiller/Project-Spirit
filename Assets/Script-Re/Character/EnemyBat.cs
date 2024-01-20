using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
    }
    protected override void Update()
    {
        if (OnAction || OnEvent || !Alive)
        {
            return;
        }
        if (onAniATK || enemyThinking || OnDashAtk)
            return;
        if (Distance() > playerDetectionRange && !onFollowPlayer)
        {
            EnemyThinking(1, 0);
            return;
        }
        if (Distance() < playerDetectionRange && onFollowPlayer && !randomMove)
        {
            randomMove = true;
            return;
        }
        if (onFollowPlayer && !randomMove && Distance() > playerDetectionRange)
        {
            MoveTo(direction.transform.position);
            Rotation();
            return;
        }
        if (randomMove && Distance() <= playerDetectionRange)
        {
            RandomMove();
            return;
        }
    }
    private void RandomMove()
    {
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 15 + direction.transform.position;
        randomMove = false;
        OnAction = true;
        onFollowPlayer = false;
        SetMoveWayPoint(point, 4);
        int i = Random.Range(0, 100);
        Debug.Log(i);
        if (i < 50)
        {
            this.DelayCall(5, () =>
            {
                if (Distance() <= characterAttack.AttackRange)
                {
                    Rotation();
                    DashAtk();
                    return;
                }
                EnemyThinking(1, 100);
            });
            return;
        }
        EnemyThinking(5, 100);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(1, 100);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        OnDashAtk = false;
        randomMove = false;
        EnemyThinking(1.3f, 100);
    }
    private void EnemyThinking(float TimeThink, int ratioRandomMove)
    {
        if (enemyThinking || !Alive)
            return;
        enemyThinking = true;
        randomMove = false;
        onFollowPlayer = false;
        this.DelayCall(TimeThink, () =>
        {
            int i = Random.Range(0, 100);
            enemyThinking = false;
            if (i < ratioRandomMove)
            {
                IsRandomMove();
                return;
            }
            onFollowPlayer = true;
        });
    }

}
