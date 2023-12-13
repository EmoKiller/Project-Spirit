
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
        slash.AddActionAttack(OnAttackHit);
        //deadAction = Dead;
        deadBody.SetActive(false);
    }
    protected override void Update()
    {
        if (OnAction)
        {
            return;
        }
        //if (onFollowPlayer && Distance() > characterAttack.AttackRange && !onAniATK ||
        //    direction != null && Distance() <= playerDetectionRange && Distance() > characterAttack.AttackRange && !onAniATK)
        //{
        //    onFollowPlayer = true;
        //    MoveTo(direction.transform.position);
        //    Rotation();
        //    return;
        //}
        if (!onFollowPlayer)
        {
            RandomMove();
            onFollowPlayer = true;
        }
        //if (onFollowPlayer && Distance() <= characterAttack.AttackRange)
        //{
        //    Rotation();
        //    characterAnimator.SetTrigger("Attack");
        //    return;
        //}
    }
    private void RandomMove()
    {
        OnAction = true;
        Vector3 vec = Random.onUnitSphere;
        Debug.Log(vec);
        Vector3 dir = vec - transform.position;
        float i = 0;
        this.LoopDelayCall(0.3f, () =>
        {
            agent.MoveToDirection(dir);
            Rotation();
            i++;
            if (i > 0.2f)
            {
                this.DelayCall(5f, () => { OnAction = false;  });
            }
        });
    }


}
