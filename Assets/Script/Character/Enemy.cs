using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBrain
{
    private Transform targetAttack = null;

    [SerializeField] protected List<Vector3> wayPoints = null;
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 15f;
    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;

    [SerializeField] private Transform tranformOfAni;
    [SerializeField] private HealthBar healthBar;

    
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        maxHealth = 100;
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
        health = maxHealth;
        healthBar.SetHealh(maxHealth);
        SetTypeSlash();
        slash.SetSizeBox(3, 1, 3);
        SetoffSlash();
        characterAnimator.AddStepAni(StartAni, SetOnSlash, SetoffSlash, FinishAni);
        slash.AddActionAttack(OnAttackHit);
        dead = Dead;
    }

    void Update()
    {

        if (arried || !Alive)
            return;

        if (onFollowPlayer && Distance() > characterAttack.AttackRange && !onAniAttck ||
            targetAttack != null && Distance() <= playerDetectionRange && Distance() > characterAttack.AttackRange && !onAniAttck)
        {
            onFollowPlayer = true;
            EnemyMove(targetAttack.transform.position);
            EnemyRotation();
            return;
        }
        if (onFollowPlayer && Distance() <= characterAttack.AttackRange)
        {
            EnemyRotation();
            characterAnimator.SetTrigger("Attack");
            return;
        }
        //SetDestination(wayPoints[currentWaypointIndex]);
        //characterAnimator.SetMovement(CharacterAnimator.MovementType.Run);
        //if (Vector3.Distance(transform.position, wayPoints[currentWaypointIndex]) <= agent.agentBody.radius)
        //    onArried?.Invoke();

    }
    public void SetTarget(Transform target)
    {
        targetAttack = target;
    }
    private float Distance()
    {
        return Vector3.Distance(transform.position, targetAttack.transform.position);
    }
    private void EnemyRotation()
    {
        Vector3 dir = targetAttack.transform.position - transform.position;
        if (dir.normalized.x > 0)
            tranformOfAni.rotation = Quaternion.Euler(-20, 180, 0);
        else if (dir.normalized.x < 0)
            tranformOfAni.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void EnemyMove(Vector3 direction)
    {
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
        characterAnimator.SetFloat("horizontal", dir.normalized.x);
        characterAnimator.SetFloat("vertical", dir.normalized.z);
    }
    protected override void StartAni()
    {
        base.StartAni();
    }
    protected override void FinishAni()
    {
        base.FinishAni();
    }
    protected virtual void OnArried()
    {
        agent.AgentBody.isStopped = true;
        arried = true;
        characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        this.DelayCall(2, () =>
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Count)
                currentWaypointIndex = 0;

            arried = false;
        });
    }
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(characterAttack.CurrentHit[0]);
        base.OnAttackHit(target);
    }
    public override void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"health: {health}\ntake damage: {damage}");
        healthBar.SetActive();
        healthBar.UpdateHealth(health);
    }

    public void OnHealhBarChanged(float value)
    {
        if(value != health)
            Debug.LogError($"some thing has changed this value: {value} - healh: {health} \"Something changed\" " + GetInstanceID());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

    }

    public override void Dead(bool a)
    {

        Destroy(gameObject);
    }

    public override void EffectHit(Vector3 dir)
    {
        AssetManager.Instance.InstantiateItems(AssetManager.Instance.SlashHit,transform, dir);
    }
}
