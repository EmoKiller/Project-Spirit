using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EneDeathCatEyeBall : Enemy
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
        characterAnimator.AddSpawnObj(SpawnObjFireBalls);
    }
    protected override void Update()
    {
        if (OnAction || OnEvent || !Alive)
        {
            return;
        }
        if (onAniATK || enemyThinking)
            return;
        if (Distance() > playerDetectionRange && !onFollowPlayer)
        {
            EnemyThinking(1, 0, null, () => { onFollowPlayer = true; onTargetPlayer = false; });
            return;
        }
        if (Distance() < playerDetectionRange && onFollowPlayer && !randomMove && !onTargetPlayer)
        {
            randomMove = true;
            onTargetPlayer = true;
            onFollowPlayer = false;
            return;
        }
        if (onFollowPlayer && !randomMove && Distance() > playerDetectionRange)
        {
            MoveTo(direction.transform.position);
            Rotation();
            onTargetPlayer = false;
            return;
        }
        if (onTargetPlayer && randomMove && Distance() <= playerDetectionRange)
        {
            RandomMove();
            return;
        }

    }
    private void RandomMove()
    {
        if (!randomMove)
            return;
        Vector3 vec = Random.onUnitSphere;
        Vector3 point = vec.normalized * 15 + direction.transform.position;
        randomMove = false;
        OnAction = true;
        onFollowPlayer = false;
        SetMoveWayPoint(point, 4);
        int i = Random.Range(0, 100);
        Debug.Log(i);
        if (i < 70)
        {
            this.DelayCall(5, () =>
            {
                OnAttack();
            });
            return;
        }
        EnemyThinking(5, 100, () => { IsRandomMove(); }, null);
    }
    protected void OnAttack()
    {
        if (Distance() <= characterAttack.AttackRangeBow)
        {
            characterAnimator.SetTrigger("UseSkill");
            return;
        }
        EnemyThinking(1, 100, () => { IsRandomMove(); }, null);
    }
    protected void SpawnObjFireBalls()
    {
        int i = Random.Range(0, 100);
        if (i < 60)
        {
            SpawnObjBallFireLoop("FireballsEnemy", 8);
            return;
        }
        if (i > 80)
        {
            SpawnObjBallFire("FireballsEnemy", 8);
            return;
        }
        SpawnObjBallFire("FireballsEnemy", GetDirection());
        return;
        
    }
    protected void SpawnObjBallFireLoop(string name, int loop)
    {
        OnAction = true;
        int i = 0;
        float euler = 0;
        Vector3 vec3 = transform.position;
        List<ObjectSkill> listObj = new List<ObjectSkill>();
        this.WaitDelayCall(loop, 0.1f, () =>
        {
            Quaternion test = Quaternion.Euler(0, euler, 0);
            Vector3 dir = test * transform.position;
            RewardSystem.Instance.SpawnObjectSkillEnemy(name, vec3 + (dir.normalized * 2) + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            listObj.Add(outSkill);
            i++;
            euler += 45;
            if (i == loop)
            {
                for (int j = 0; j < listObj.Count; j++)
                {
                    Vector3 direc = (direction.position + new Vector3(0, 1.5f, 0)) - listObj[j].transform.position;
                    listObj[j].transform.DOMove(listObj[j].transform.position + (direc.normalized * 50), 4f);
                }
                OnAction = false;
                FinishAniAtk();
            }
        });
    }
    protected void SpawnObjBallFire(string name,int loop)
    {
        OnAction = true;
        int i = 0;
        float euler = 0;
        this.WaitDelayCall(loop, 0.5f, () =>
        {
            Quaternion test = Quaternion.Euler(0, euler, 0);
            Vector3 dir = test * transform.position;
            RewardSystem.Instance.SpawnObjectSkillEnemy(name, transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            outSkill.transform.DOMove(transform.position + (dir.normalized * 50) + new Vector3(0, 1.5f, 0), 4f);
            i++;
            euler += 45;
            if (i == loop)
            {
                OnAction = false;
                FinishAniAtk();
            }
        });
    }
    protected void SpawnObjBallFire(string name, Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy(name, transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.Init(1f, 4);
        outSkill.transform.DOMove(transform.position + (foward.normalized * 50) + new Vector3(0, 1.5f, 0), 4f);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        randomMove = false;
        if (Distance() > playerDetectionRange)
        {
            EnemyThinking(1, 0, null, () => { onFollowPlayer = true; });
            return;
        }
        EnemyThinking(2, 30, () => { IsRandomMove(); }, () => { OnAttack(); });
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
