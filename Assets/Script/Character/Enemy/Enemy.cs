using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBrain
{
    [SerializeField] protected List<Vector3> wayPoints = null;
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 15f;
    [SerializeField] protected bool onFollowPlayer = false;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected GameObject deadBody;
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void Start()
    {
        SetTypeSlash("Enemy");
        characterAttack.Initialized();
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
    }
    protected virtual void Update()
    {
        if (!Alive || OnAction)
            return;
        if (onFollowPlayer && Distance() > characterAttack.AttackRange && !onAniAttck ||
            direction != null && Distance() <= playerDetectionRange && Distance() > characterAttack.AttackRange && !onAniAttck)
        {
            onFollowPlayer = true;
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
    public void Push()
    {
        if (Distance() <= characterAttack.AttackRange)
        {
            characterAnimator.SetTrigger("Attack");
        }
    }
    public float Distance()
    {
        return Vector3.Distance(transform.position, direction.transform.position);
    }
    protected override void StartAniAtk()
    {
        base.StartAniAtk();
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
    }
    protected virtual void OnArried()
    {
        agent.AgentBody.isStopped = true;
        OnAction = true;
        characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        this.DelayCall(2, () =>
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Count)
                currentWaypointIndex = 0;

            OnAction = false;
        });
    }
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(characterAttack.CurrentHit[0]);
        base.OnAttackHit(target);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        health -= damage;
        healthBar.SetActive();
        healthBar.UpdateHealth(health);
        if (health <= 0)
        {
            Dead();
        }
    }
    public override void Dead()
    {
        Vector3 dir = transform.position - direction.position;
        ImpactForce(dir.normalized * 20);
        deadBody.SetActive(true);
        healthBar.gameObject.SetActive(false);
        tranformOfAni.SetActive(false);
        slash.gameObject.SetActive(false);
        deadBody.transform.DOLocalMoveY(1.5f, 0.1f).OnComplete(() =>
        {
            deadBody.transform.DOLocalMoveY(0, 0.4f).OnComplete(() =>
            {
                
            });
        });
    }
    public override void EffectHit(Vector3 dir)
    {
        //Debug.Log(GameConstants.Slash);
        //AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }
}
