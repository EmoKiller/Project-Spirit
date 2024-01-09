using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EneScampArcher : Enemy
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
        characterAnimator.AddSpawnObj(SpawnObjBow);
        playerDetectionRange = 20;
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
            EnemyThinking(1, 0, null, () => { onFollowPlayer = true; });
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
                OnAttackBow();
            });
        }
        else
        {
            EnemyThinking(5, 100, () => { IsRandomMove(); }, null);
        }
    }
    protected void OnAttackBow()
    {
        if (Distance() <= characterAttack.AttackRange)
        {
            Rotation();
            characterAnimator.SetTrigger("Attack");
            return;
        }
        if (Distance() <= characterAttack.AttackRangeBow)
        {
            characterAnimator.SetTrigger("BowAttack");
            return;
        }
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    protected void SpawnObjBow()
    {
        SpawnObjBow("ArrowEnemy", GetDirection());
    }
    protected void SpawnObjBow(string name, Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy(name, transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);

        outSkill.transform.rotation = Quaternion.LookRotation(foward);
        outSkill.Init(1f, 2);
        outSkill.transform.DOMove(transform.position + (foward.normalized * 50) + new Vector3(0, 1.5f, 0), 2f);
    }
    protected override void EventInDashAtks()
    {
        agent.moveSpeed = 18;
        SetMoveWayPoint(direction.transform.position, 4f);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        OnDashAtk = false;
        randomMove = false;
        EnemyThinking(1, 30, () => { IsRandomMove(); }, () => { OnAttackBow(); });
    }
    //private void EnemyThinking(float TimeThink, int ratioRandomMove, UnityAction actionRandomMove, UnityAction action2)
    //{
    //    if (enemyThinking || !Alive)
    //        return;
    //    enemyThinking = true;
    //    randomMove = false;
    //    onFollowPlayer = false;
    //    this.DelayCall(TimeThink, () =>
    //    {
    //        int i = Random.Range(0, 100);
    //        enemyThinking = false;
    //        if (i < ratioRandomMove)
    //        {
    //            actionRandomMove?.Invoke();
    //            return;
    //        }
    //        action2?.Invoke();
    //    });
    //}

    private void IsRandomMove()
    {
        randomMove = true;
        onFollowPlayer = false;
    }
}
