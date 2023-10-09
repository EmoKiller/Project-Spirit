using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBrain
{
    [Header("Attack")]
    [SerializeField] protected float attackRange = 5f;
    protected override CharacterBrain targetAttack => GameManager.Instance.player;

    Vector3 attackPosition = Vector3.zero;
    private float distance => Vector3.Distance(transform.position, targetAttack.transform.position);
    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;

    protected override void Awake()
    {
       
        base.Awake();
        agent.OnArried = OnArried;
        
    }
    void Update()
    {
        if (arried)
            return;
        if (targetAttack != null && distance <= attackRange && distance > characterAttack.AttackRange)
        {
            onFollowPlayer = true;
            agent.agentBody.isStopped = true;
            SetDestination(targetAttack.transform.position);
            return;
        }
        else if (distance <= characterAttack.AttackRange)
        {
            Debug.Log("Atk");
        }
        //if (onFollowPlayer && targetAttack != null && Vector3.Distance(transform.position, targetAttack.transform.position) > attackRange)
        //{
            
        //    agent.SetDestination(targetAttack.transform.position);
        //    //return;
        //}
    }
    public void SetDestination(Vector3 direction)
    {
        agent.agentBody.isStopped = false;
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir);

    }
    protected virtual void OnArried()
    {
        arried = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
