using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : CharacterBrain
{
    private Transform targetAttack = null;

    [SerializeField] protected List<Vector3> wayPoints = null;
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 15f;
    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;

    [SerializeField] private Transform TranfomThis;
    [SerializeField] private HealthBar healthBar;

    protected float Distance => Vector3.Distance(transform.position, targetAttack.transform.position);
    [SerializeField] private bool onAniAttck = false;
    protected override void Awake()
    {
        base.Awake();
        agent.onArried = OnAttack;
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        MaxHealth = 100;
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
        Health = MaxHealth;
        healthBar.SetHealh(MaxHealth);

    }

    void Update()
    {

        if (arried || !Alive)
            return;

        if (onFollowPlayer && Distance > characterAttack.AttackRange && !onAniAttck ||
            targetAttack != null && Distance <= playerDetectionRange && Distance > characterAttack.AttackRange && !onAniAttck)
        {
            onFollowPlayer = true;
            EnemyMove(targetAttack.transform.position);
            EnemyRotation();
            return;
        }
        if (onFollowPlayer && Distance <= characterAttack.AttackRange)
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
    private void EnemyRotation()
    {
        Vector3 dir = targetAttack.transform.position - transform.position;
        if (dir.normalized.x > 0)
            TranfomThis.rotation = Quaternion.Euler(-20, 180, 0);
        else if (dir.normalized.x < 0)
            TranfomThis.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void EnemyMove(Vector3 direction)
    {
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
        characterAnimator.SetFloat("horizontal", dir.normalized.x);
        characterAnimator.SetFloat("vertical", dir.normalized.z);
    }
    private void OnStartAttack()
    {
        onAniAttck = true;
    }
    private void OnAttack()
    {
        if (Distance <= characterAttack.AttackRange + 1f)
        {
            Debug.Log("Enemy Hit Player");
        }
        else
            Debug.Log("Enemy-Miss");
        onAniAttck = false;
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
    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnEnemyAttack, OnAttack);
        EventDispatcher.AddListener(Events.OnEnemyStartAttack, OnStartAttack);
    }
    private void OnDisable()
    {
        EventDispatcher.AddListener(Events.OnEnemyAttack, OnAttack);
        EventDispatcher.AddListener(Events.OnEnemyStartAttack, OnStartAttack);
    }
    public override void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log($"health: {Health}\ntake damage: {damage}");
        healthBar.SetActive();
        healthBar.UpdateHealth(Health);
    }

    public void OnHealhBarChanged(float value)
    {
        if(value != Health)
            Debug.LogError($"some thing has changed this value: {value} - healh: {Health} \"Something changed\" " + GetInstanceID());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

    }

    public override void StartAttack(float damage)
    {
        throw new NotImplementedException();
    }
}
