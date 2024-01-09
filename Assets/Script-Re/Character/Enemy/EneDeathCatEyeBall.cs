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
                OnAttack();
            });
        }
        else
        {
            EnemyThinking(5, 100, () => { IsRandomMove(); }, null);
        }
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
        //SpawnObjBallFireLoop("FireballsEnemy", 8);
        //return;
        int i = Random.Range(0, 100);
        if (i < 45)
        {
            SpawnObjBallFire("FireballsEnemy", GetDirection());
            return;
        }
        if (i > 65)
        {
            SpawnObjBallFire("FireballsEnemy", 8);
            return;
        }

        SpawnObjBallFireLoop("FireballsEnemy", 8);
    }
    protected void SpawnObjBallFireLoop(string name, int loop)
    {
        OnAction = true;
        int i = 0;
        float euler = 0;
        List<ObjectSkill> listObj = new List<ObjectSkill>();
        this.WaitDelayCall(loop, 0.5f, () =>
        {
            Quaternion test = Quaternion.Euler(0, euler, 0);
            Vector3 dir = test * transform.position;
            RewardSystem.Instance.SpawnObjectSkillEnemy(name, (dir.normalized * 2) + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            listObj.Add(outSkill);
            i++;
            euler += 45;
            if (i == loop)
            {
                for (int j = 0; j < listObj.Count; j++)
                {
                    listObj[j].transform.DOMove(transform.position + (GetDirection().normalized * 50) + new Vector3(0, 1.5f, 0), 4f);
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
    protected override void EventInDashAtks()
    {
        agent.moveSpeed = 18;
        SetMoveWayPoint(direction.transform.position, 4f);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = 4;
        OnDashAtk = false;
        randomMove = false;
        EnemyThinking(1, 30, () => { IsRandomMove(); }, () => { OnAttack(); });
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
