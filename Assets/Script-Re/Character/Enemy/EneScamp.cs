
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
        characterAnimator.SetTrigger("Idie");
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 5 + transform.position;
        OnAction = true;
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
        agent.moveSpeed = 4;
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
        this.DelayCall(3f, () => { EnemyThinking(1, 5); });
    }
    public override void MoveTo(Vector3 direction)
    {
        base.MoveTo(direction);
    }

    //protected override void Start()
    //{
    //    base.Start();
    //    Init();
    //}
    //public override void Init()
    //{
    //    base.Init();
    //    characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
    //    characterAnimator.AddDashAtk(EventInDashAtks);
    //}
    //protected override void EnemyIdle()
    //{

    //}
    //protected override void EnemyChase()
    //{
    //    //EnemyMoveToTarget();
    //    //if (onFollowPlayer && Distance() <= characterAttack.AttackRange)
    //    //{
    //    //    Rotation();
    //    //    characterAnimator.SetTrigger("Attack");
    //    //    return;
    //    //}
    //}
    //protected override void EnemyRandomMove()
    //{
    //    if (randomMove == true)
    //        return;
    //    base.EnemyRandomMove();
    //    ThinkOfScamp(6, 100);
    //}
    //public override void TakeDamage(float damage)
    //{
    //    base.TakeDamage(damage);
    //    ThinkOfScamp(1, 60);
    //}
    //protected override void FinishAniAtk()
    //{
    //    base.FinishAniAtk();
    //    agent.moveSpeed = 4;
    //    ThinkOfScamp(1.3f, 75);
    //}
    //private void ThinkOfScamp(float time, int ratio)
    //{
    //    EnemyThinking(time, ratio,
    //        () => { characterAnimator.SetAnimationState(CharacterAnimator.AnimationStates.RandomMove); },
    //        () => { characterAnimator.SetAnimationState(CharacterAnimator.AnimationStates.Chase); });
    //}
    //private void IsFollowAtK()
    //{
    //    randomMove = false;
    //    onFollowPlayer = true;
    //    int i = Random.Range(0, 100);
    //    if (i < 50)
    //    {
    //        characterAnimator.SetTrigger("RunFollow");
    //        agent.moveSpeed = 8f;
    //        enemyRunFollow = true;
    //        return;
    //    }
    //    OnDashAtk = true;
    //}

    //protected override void Update()
    //{

    //    if (OnAction || OnEvent || !Alive)
    //    {
    //        return;
    //    }
    //    if (onAniATK)
    //        return;
    //    //base.Update();
    //    if (Distance() > playerDetectionRange && !enemyRunFollow)
    //    {
    //        EnemyThinking(1, 25);
    //    }
    //    if (onFollowPlayer && Distance() < DashAttackRange && OnDashAtk && !enemyRunFollow)
    //    {
    //        DashAtk();
    //        return;
    //    }
    //    if (OnDashAtk)
    //        return;
    //    if (Distance() <= playerDetectionRange - 1 && Distance() <= playerDetectionRange + 1 && randomMove)
    //    {
    //        RandomMove();
    //        return;
    //    }
    //    if (onFollowPlayer && Distance() > characterAttack.AttackRange && !randomMove)
    //    {
    //        randomMove = false;
    //        MoveTo(direction.transform.position);
    //        Rotation();
    //        return;
    //    }

    //    if (onFollowPlayer && Distance() <= characterAttack.AttackRange)
    //    {
    //        Rotation();
    //        characterAnimator.SetTrigger("Attack");
    //        return;
    //    }
    //}
    //private void RandomMove()
    //{
    //    onFollowPlayer = false;
    //    randomMove = false;
    //    characterAnimator.SetTrigger("Idie");
    //    Vector3 vec = Random.onUnitSphere;
    //    Vector3 point = vec.normalized * 5 + transform.position;
    //    OnAction = true;
    //    SetMoveWayPoint(point, 3);
    //    EnemyThinking(3, 70);
    //}
    //private void EnemyThinking(float TimeThink, int ratioRandomMove)
    //{
    //    if (enemyThinking || !Alive)
    //        return;
    //    characterAnimator.SetTrigger("Idie");
    //    enemyThinking = true;
    //    OnDashAtk = false;
    //    onFollowPlayer = false;
    //    enemyRunFollow = false;
    //    this.DelayCall(TimeThink, () =>
    //    {
    //        int i = Random.Range(0, 100);
    //        enemyThinking = false;
    //        if (i < ratioRandomMove)
    //        {
    //            IsRandomMove();
    //            return;
    //        }
    //        IsFollowAtK();
    //    });
    //}
    //private void IsRandomMove()
    //{
    //    randomMove = true;
    //    onFollowPlayer = false;
    //}

}



