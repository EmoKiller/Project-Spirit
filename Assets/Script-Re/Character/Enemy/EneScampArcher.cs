using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EneScampArcher : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        characterAnimator.AddSpawnObj(SpawnObjBow);
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
        {
            this.DelayCall(5, () => { OnAttackBow(); });
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
        EnemyThinking(1, 30, () => { IsRandomMove(); }, () => { OnAttackBow(); });
    }
}
