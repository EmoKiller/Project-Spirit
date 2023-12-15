
using UnityEngine;

public class EneScamp : Enemy
{

    protected override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        maxHealth = characterAttack.HP;
        health = maxHealth;
        healthBar.SetHealh(maxHealth);
        SetoffSlash();
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        slash.AddActionAttack(OnAttackHit);
        deadBody.SetActive(false);
        
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
            EnemyThinking(1,25);
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
        if (onFollowPlayer && Distance() > characterAttack.AttackRange && !randomMove )
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
    private void DashAtk()
    {
        OnDashAtk = true;
        characterAnimator.SetTrigger("DashAttack");
    }
    private void EventInDashAtks()
    {
        agent.moveSpeed = 10;
        SetMoveWayPoint(direction.transform.position, 2);
    }
    private void RandomMove()
    {
        onFollowPlayer = false;
        randomMove = false;
        characterAnimator.SetTrigger("Idie");
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 5  + transform.position;
        OnAction = true;
        SetMoveWayPoint(point, 3);
        EnemyThinking(3,70);
    }
    public override void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        base.SetMoveWayPoint(wayPoint, time);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(1,80);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        EnemyThinking(1.3f,75);
    }
    private void EnemyThinking(float TimeThink, int ratioRandomMove)
    {
        if (enemyThinking || !Alive)
            return;
        enemyThinking = true;
        OnDashAtk = false;
        onFollowPlayer = false;
        enemyRunFollow = false;
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
    
    private void IsRandomMove()
    {
        randomMove = true;
        onFollowPlayer = false;
    }
    private void IsFollowAtK()
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
        characterAnimator.SetTrigger("Idie");
        Debug.Log("Dash Atk");
    }
    public override void Dead()
    {
        base.Dead();
    }

}
