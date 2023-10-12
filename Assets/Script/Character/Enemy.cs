using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterBrain
{
    protected override CharacterBrain targetAttack => GameManager.Instance.player;
    [Header("Attack")]
    [SerializeField] protected float playerDetectionRange = 5f;
    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;
    [SerializeField] protected Slider sliderHp;
    [SerializeField] protected List<Vector3> wayPoints = null;
    [SerializeField] protected int currentWaypointIndex = 0;
    

    protected override Vector3 direction => targetAttack.transform.position;
    protected float distance => Vector3.Distance(transform.position, targetAttack.transform.position);


    protected Action onArried = null;


    protected override void Awake()
    {
        
        base.Awake();
        onArried = OnArried;
        
    }
    private void Start()
    {
        wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
    }
    void Update()
    {
        
        if (arried)
            return;
        if (targetAttack != null && distance <= playerDetectionRange && distance > characterAttack.AttackRange)
        {
            onFollowPlayer = true;
            SetDestination(targetAttack.transform.position);
            charactorDirectionMove.DirectionMove(transform.position, targetAttack.transform.position, dirNum);
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Walk);
            return;
        }
        if (onFollowPlayer && distance <= characterAttack.AttackRange)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
            DoAttack();
            return;
        }
        SetDestination(wayPoints[currentWaypointIndex]);
        charactorDirectionMove.DirectionMove(transform.position, wayPoints[currentWaypointIndex], dirNum);
        characterAnimator.SetMovement(CharacterAnimator.MovementType.Walk);
        if (Vector3.Distance(transform.position, wayPoints[currentWaypointIndex]) <= agent.agentBody.radius)
            onArried?.Invoke();
        
    }
    public void SetDestination(Vector3 direction)
    {
        agent.agentBody.isStopped = false;
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }
}
