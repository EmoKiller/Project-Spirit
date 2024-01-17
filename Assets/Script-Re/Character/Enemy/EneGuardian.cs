using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneGuardian : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        characterAnimator.AddSpawnObj(SpawnChain);
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
        //if (onFollowPlayer && Distance() < DashAttackRange && OnDashAtk && !enemyRunFollow)
        //{
        //    DashAtk();
        //    return;
        //}
        //if (OnDashAtk)
        //    return;
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
                return;
            }
            IsFollowAtK();
        });
    }
    private void RandomMove()
    {
        if (!randomMove)
            return;
        characterAnimator.SetTrigger("Move");
        onFollowPlayer = false;
        randomMove = false;
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 15 + direction.transform.position;
        SetMoveWayPoint(point, 3);
        EnemyThinking(3, 35);
    }
    protected void IsFollowAtK()
    {
        randomMove = false;
        onFollowPlayer = true;
        int i = Random.Range(0, 100);
        if (i < 60)
        {
            characterAnimator.SetTrigger("RunFollow");
            agent.moveSpeed = 8f;
            enemyRunFollow = true;
            return;
        }
        
        this.DelayCall(3f, () => { EnemyThinking(1, 5); });
    }
    protected void OnUseSKill()
    {
        if (Distance() <= characterAttack.AttackRangeBow)
        {
            characterAnimator.SetTrigger("UseSkill");
            return;
        }
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    private void SpawnChain()
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy("ObjRingDeadEnemy", direction.position , out ObjectSkill outSkill);
        outSkill.Init(1,true);
    }
    //protected void SpawnObjFireBalls()
    //{
    //    int i = Random.Range(0, 100);
    //    if (i < 60)
    //    {
    //        SpawnObjBallFireLoop(8);
    //        return;
    //    }
    //    if (i > 80)
    //    {
    //        SpawnObjBallFire(8);
    //        return;
    //    }
    //    SpawnObjBallFire();
    //    return;
    //}
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    protected override void FinishAniAtk()
    {
        characterAnimator.SetTrigger("Idie");
        base.FinishAniAtk();
        EnemyThinking(1f, 35);
    }
}
