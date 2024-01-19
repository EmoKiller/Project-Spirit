using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRe : CharacterBrain
{
    #region State Machine var
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    #endregion
    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }
    protected override void Start()
    {
        CurrentHealth = maxHealth;
    }
    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    #region Animation Trigger
    private void AnimationTriggerEvent(CharacterAnimator.AnimationStates triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }
    #endregion

     
    public override void Rolling()
    {
        throw new System.NotImplementedException();
    }

    protected override void EffectHit(Vector3 dir)
    {
        throw new System.NotImplementedException();
    }
}
