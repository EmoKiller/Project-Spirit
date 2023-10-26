using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : CharacterBrain
{
    public CharacterBrain targetAttack = null;

    [Header("Attack")]
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 15f;

    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;

    [SerializeField] protected List<Vector3> wayPoints = null;
    public Transform TranfomThis;
    public ShowHPEnemy sliderHp;
    [SerializeField] private HealthBar healthBar;

    protected float distance => Vector3.Distance(transform.position, targetAttack.transform.position);
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
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
        health = maxHealth;
        sliderHp.UpdateSlider(maxHealth);
        sliderHp.gameObject.SetActive(false);
        //healthBar.SetHealh(maxHealth);

    }

    void Update()
    {

        if (arried || !Alive)
            return;

        if (onFollowPlayer && distance > characterAttack.AttackRange && !onAniAttck ||
            targetAttack != null && distance <= playerDetectionRange && distance > characterAttack.AttackRange && !onAniAttck)
        {
            onFollowPlayer = true;
            SetDestination(targetAttack.transform.position);
            EnemyRotation();
            return;
        }
        if (onFollowPlayer && distance <= characterAttack.AttackRange)
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
    private void EnemyRotation()
    {
        Vector3 dir = targetAttack.transform.position - transform.position;
        if (dir.normalized.x > 0)
            TranfomThis.rotation = Quaternion.Euler(-20, 180, 0);
        else if (dir.normalized.x < 0)
            TranfomThis.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void SetDestination(Vector3 direction)
    {
        agent.agentBody.isStopped = false;
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
        if (distance <= characterAttack.AttackRange + 1f)
        {
            Debug.Log("Enemy Hit Player");
        }
        else
            Debug.Log("Enemy-Miss");
        onAniAttck = false;
    }
    protected virtual void OnArried()
    {
        agent.agentBody.isStopped = true;
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
        health -= damage;
        Debug.Log($"health: {health}\ntake damage: {damage}");
        sliderHp.gameObject.SetActive(true);
        sliderHp.UpdateHealth(health);
        //healthBar.UpdateHealth(health);
    }

    public void OnHealhBarChanged(float value)
    {
        if(value != health)
            Debug.LogError($"some thing has changed this value: {value} -   healh: {health} \"Something changed\" " + GetInstanceID());
        
    }
    public void OnHealhBarChanged2(float value)
    {
        if (value != health)
            Debug.LogError($"some thing has changed this value: {value} -   healh: {health} \"Something changed\" " + GetInstanceID());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

    }

}
