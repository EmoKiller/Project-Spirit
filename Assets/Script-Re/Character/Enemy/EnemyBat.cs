using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    protected override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        playerDetectionRange = 20;
    }
    protected override void Update()
    {
        if (OnAction || OnEvent || !Alive)
        {
            return;
        }
        if (OnDashAtk || enemyThinking)
            return;
        //if (Distance() > playerDetectionRange && !onFollowPlayer)
        //{
        //    EnemyThinking(1, 25);
        //    return;
        //}
        if (randomMove && Distance() <= playerDetectionRange + 10)
        {
            RandomMove();
            return;
        }
        if (!randomMove && Distance() > playerDetectionRange)
        {
            MoveTo(direction.transform.position);
            Rotation();
        }
        else
        {
            randomMove = true;
        }
    }
    private void RandomMove()
    {
        if (randomMove == true)
            return;
        randomMove = true;
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 15 + direction.transform.position;
        OnAction = true;
        SetMoveWayPoint(point, 4);
        int i = Random.Range(0, 100);
        if (i < 50)
        {
            this.DelayCall(4, () =>
            {
                if (onFollowPlayer && Distance() <= characterAttack.AttackRange && enemyRunFollow)
                {
                    Rotation();
                    DashAtk();
                }
            });
            return;
        }
        EnemyThinking(4, 50);

    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(1, 60);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        OnDashAtk = false;
        EnemyThinking(1.3f, 75);
    }
    private void EnemyThinking(float TimeThink, int ratioRandomMove)
    {
        if (enemyThinking || !Alive)
            return;
        enemyThinking = true;
        randomMove = false;
        this.DelayCall(TimeThink, () =>
        {
            enemyThinking = false;
            IsRandomMove();
        });
    }

    private void IsRandomMove()
    {
        randomMove = true;
        onFollowPlayer = false;
    }
    protected override void Dead()
    {
        base.Dead();
    }
}
