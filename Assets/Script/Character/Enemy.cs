using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterBrain 
{
    public CharacterBrain targetAttack = null;
    protected override Vector3 direction => targetAttack.transform.position;
    protected override bool Alive => sliderHp.sliders.value > 0;

    [Header("Attack")]
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 5f;

    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;

    [SerializeField] protected ChildrenSlider sliderHp;

    [SerializeField] protected List<Vector3> wayPoints = null;

    protected float distance => Vector3.Distance(transform.position, targetAttack.transform.position);


    protected Action onArried = null;
    protected override void Awake()
    {
        
        base.Awake();
        //onArried = OnArried;
        
    }
    private void Start()
    {
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
        sliderHp.UpdateSlider(3);
        sliderHp.gameObject.SetActive(false);
    }
    void Update()
    {
        if (arried && Alive)
            return;
        if (targetAttack != null && distance <= playerDetectionRange && distance > characterAttack.AttackRange)
        {
            onFollowPlayer = true;
            SetDestination(targetAttack.transform.position);
           
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Run);
            return;
        }
        if (onFollowPlayer && distance <= characterAttack.AttackRange)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
            //DoAttack();
            return;
        }
        //SetDestination(wayPoints[currentWaypointIndex]);
        characterAnimator.SetMovement(CharacterAnimator.MovementType.Run);
        //if (Vector3.Distance(transform.position, wayPoints[currentWaypointIndex]) <= agent.agentBody.radius)
        //    onArried?.Invoke();
        
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

    //private void OnEnable()
    //{
    //    EventDispatcher.AddListener(Events.OnAttack, OnHit);
    //}
    //private void OnDisable()
    //{
    //    EventDispatcher.RemoveListener(Events.OnAttack, OnHit);
    //}
    //public override void OnHit()
    //{
    //    Debug.Log("enemy Trigger OnHit");
    //    sliderHp.OnReduceValueChanged(targetAttack.CharacterAtk.Damage);
    //    sliderHp.gameObject.SetActive(true);
    //    if (!Alive)
    //    {
    //        Debug.Log("Enemy Dead");
    //    }


    //}
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
    }
}
