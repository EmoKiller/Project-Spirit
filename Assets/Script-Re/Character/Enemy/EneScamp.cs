
using UnityEngine;

public class EneScamp : Enemy
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
        if (onAniATK)
            return;
        if (Distance() > playerDetectionRange && !enemyRunFollow)
        {
            EnemyThinking(1, 25);
        }
        if (onFollowPlayer && Distance() < DashAttackRange && OnDashAtk && !enemyRunFollow)
        {
            DashAtk();
            return;
        }
        if (OnDashAtk)
            return;
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
    private void RandomMove()
    {
        onFollowPlayer = false;
        randomMove = false;
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 5 + transform.position;
        SetMoveWayPoint(point, 3);
        EnemyThinking(3, 70);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(1, 60);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        EnemyThinking(1.3f, 75);
    }
    private void EnemyThinking(float TimeThink, int ratioRandomMove)
    {
        if (enemyThinking || !Alive)
            return;
        characterAnimator.SetTrigger("Idie");
        enemyThinking = true;
        OnDashAtk = false;
        onFollowPlayer = false;
        enemyRunFollow = false;
        agent.moveSpeed = characterAttack.NomalSpeed;
        this.DelayCall(TimeThink, () =>
        {
            int i = Random.Range(0, 100);
            enemyThinking = false;
            if (i < ratioRandomMove)
            {
                IsRandomMove();
                return;
            }
            IsFollowAtK();
        });
    }
    protected void IsFollowAtK()
    {
        randomMove = false;
        onFollowPlayer = true;
        int i = Random.Range(0, 100);
        if (i < 50)
        {
            characterAnimator.SetTrigger("RunFollow");
            agent.moveSpeed = 8f;
            enemyRunFollow = true;
            return;
        }
        OnDashAtk = true;
        this.DelayCall(3f, () => { EnemyThinking(1, 5); });
    }
}



