
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
        //characterAnimator.AddDashAtk();
        slash.AddActionAttack(OnAttackHit);
        //deadAction = Dead;
        deadBody.SetActive(false);
        
    }
    protected override void Update()
    {
        if (OnAction || OnEvent)
        {
            return;
        }
        if (onAniATK)
            return;
        if (Distance() > playerDetectionRange)
        {
            EnemyThinking(30);
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
        //if (onFollowPlayer && Distance() <= characterAttack.AttackRange + 2)
        //{
        //    int i = Random.Range(0, 100);
        //    if (i < 50)
        //    {
        //        characterAnimator.AddDashAtk(DashAtk);
        //        Debug.Log("DaskATK");
        //    }
        //}
        if (onFollowPlayer && Distance() <= characterAttack.AttackRange)
        {
            Rotation();
            characterAnimator.SetTrigger("Attack");
            return;
        }
    }
    private void DashAtk()
    {
        characterAnimator.SetTrigger("Attack");
        SetMoveWayPoint(direction.transform.position * 2, 2);
    }
    private void RandomMove()
    {
        onFollowPlayer = false;
        randomMove = false;
        characterAnimator.SetTrigger("Idie");
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 4  + transform.position;
        OnAction = true;
        SetMoveWayPoint(point, 2);
        EnemyThinking(70);
    }
    public override void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        base.SetMoveWayPoint(wayPoint, time);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(80);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        EnemyThinking(80);
    }
    private void EnemyThinking(int ratioRandomMove)
    {
        if (enemyThinking)
            return;
        enemyThinking = true;
        characterAnimator.AddDashAtk(null);
        this.DelayCall(2, () =>
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
            agent.moveSpeed = 5.5f;
        }

    }

}
