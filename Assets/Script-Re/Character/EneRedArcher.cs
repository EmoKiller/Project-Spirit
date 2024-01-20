using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneRedArcher : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        characterAnimator.AddSpawnObj(OnBowAttack);
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
        if (i < 50)
            this.DelayCall(5, () => { OnAttackBow(); });
        else
            EnemyThinking(5, 100, () => { IsRandomMove(); }, null);
    }
    protected void OnAttackBow()
    {
        Rotation();
        if (Distance() <= characterAttack.AttackRange)
        {
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
    protected void OnBowAttack()
    {
        SpawnObjBow(3,10);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        OnDashAtk = false;
        randomMove = false;
        EnemyThinking(1.5f, 30, () => { IsRandomMove(); }, () => { OnAttackBow(); });
    }
}
