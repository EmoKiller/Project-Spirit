using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterBrain
{
    [Header("Attack")]
    [SerializeField] protected float attackRange = 5f;
    protected override CharacterBrain targetAttack => GameManager.Instance.player;
    private float distance => Vector3.Distance(transform.position, targetAttack.transform.position);
    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool onFollowPlayer = false;
    [SerializeField] protected Slider sliderHp;

    public Slider SliderHp => sliderHp;
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
            charactorDirectionMove.DirectionMove(transform.position,targetAttack.transform.position,dirMove);
            onFollowPlayer = true;
            agent.agentBody.isStopped = true;
            SetDestination(targetAttack.transform.position);
            return;
        }
        else if (distance <= characterAttack.AttackRange)
        {
            //Debug.Log("Atk");
        }
        
        //if (onFollowPlayer && targetAttack != null && Vector3.Distance(transform.position, targetAttack.transform.position) > attackRange)
        //{

        //    agent.SetDestination(targetAttack.transform.position);
        //    //return;
        //}
    }
    private void SetDestination(Vector3 direction)
    {
        agent.agentBody.isStopped = false;
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir);

    }
    //private void DirectionMove()
    //{
    //    if (Mathf.Abs(targetAttack.transform.position.x - transform.position.x) > Mathf.Abs(targetAttack.transform.position.z - transform.position.z))
    //    {
    //        dirMove = transform.position.x < targetAttack.transform.position.x ? 3 : 2;
    //    }
    //    else if (Mathf.Abs(targetAttack.transform.position.x - transform.position.x) < Mathf.Abs(targetAttack.transform.position.z - transform.position.z))
    //    {
    //        dirMove = transform.position.z < targetAttack.transform.position.z ? 1 : 0;
    //    }
    //    charactorDirMove.SetActiveDirectionMove((DirectionMove)dirMove);
    //}

    protected virtual void OnArried()
    {
        arried = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
