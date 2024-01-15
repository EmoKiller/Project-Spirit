using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneHealer : Enemy
{
    private void Awake()
    {
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        characterAnimator.AddDashAtk(EventInDashAtks);
        characterAnimator.AddSpawnObj(SpawnObjBoom);
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
        if (Distance() <= characterAttack.AttackRangeBow)
        {
            characterAnimator.SetTrigger("BowAttack");
            return;
        }
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    protected void SpawnObjBoom()
    {
        SpawnObj("ObjBoomEnemy", direction.position);
    }
    protected void SpawnObj(string name, Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy(name, transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.Init(1f, true);
        outSkill.transform.DOJump(foward, 3f, 1, 0.7f).OnComplete(() =>
        {
            outSkill.ActiveBoom();
        });
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
        EnemyThinking(2f, 30, () => { IsRandomMove(); }, () => { OnAttackBow(); });
    }
}
