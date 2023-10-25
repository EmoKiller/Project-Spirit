using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : CharacterBrain 
{
    public CharacterBrain targetAttack = null;
    protected override Vector3 direction => targetAttack.transform.position;
    public override bool Alive => sliderHp.sliders.value > 0;

    [Header("Attack")]
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 15f;

    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;

    [SerializeField] protected List<Vector3> wayPoints = null;
    public Transform TranfomThis;
    public ChildrenSlider sliderHp;

    protected float distance => Vector3.Distance(transform.position, targetAttack.transform.position);
    [SerializeField] private bool onAniAttck = false;
    protected override void Awake()
    {
        base.Awake();
        //agent.onArried = OnArried;
        agent.onArried = OnAttack;
    }
    private void Start()
    {
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
        sliderHp.UpdateSlider(100);
        sliderHp.gameObject.SetActive(false);
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
        else if(dir.normalized.x < 0)
            TranfomThis.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void SetDestination(Vector3 direction)
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
    public virtual void OnAttack()
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
            if(currentWaypointIndex >= wayPoints.Count)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

    }
}
