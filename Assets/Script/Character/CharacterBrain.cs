using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected Slash slash = null;
    [SerializeField] protected Transform direction;
    [SerializeField] protected AnimationCurve forceCurve;
    //BaseCharacter
    protected string characterName {get; set;}
    protected float health { get; set; }
    protected float maxHealth { get; set; }
    protected bool onAniAttck = false;
    protected Action<bool> deadAction = null;
    public bool Alive => health >= 0;
    public virtual string Name => characterName;
    
    protected virtual void Awake()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterAttack.Initialized();
        characterName = gameObject.name;
    }
    protected void SetTypeSlash(string value)
    {
        slash.SetType(value);
    }
    protected virtual void SetOnSlash()
    {
        slash.SetActiveSlash(true);
    }
    protected virtual void SetoffSlash()
    {
        slash.SetActiveSlash(false);
    }
    protected virtual void StartAni()
    {
        onAniAttck = true;
    }
    protected virtual void FinishAni()
    {
        characterAnimator.ResetTrigger();
        onAniAttck = false;
    }
    protected virtual void OnAttackHit(CharacterBrain target)
    {
        Vector3 dir = transform.position - target.transform.position;
        float force = target.characterAttack.Weight - characterAttack.PowerForce;
        if (force > 0)
            ImpactForce(dir.normalized * force);
        else
            target.ImpactForce(dir.normalized * force);
        target.EffectHit(dir.normalized + target.transform.position);
        //EventDispatcher.TriggerEvent(Events.OnEnemyHit);
        if (!target.Alive)
        {
            //EventDispatcher.TriggerEvent(Events.OnEnemyDead);
        }
    }
    public abstract void TakeDamage(float damage);
    public abstract void EffectHit(Vector3 dir);
    public abstract void Dead(bool isDead);
    public void ImpactForce(Vector3 dir)
    {
        this.LoopDelayCall(0.3f, () =>
        {
            agent.AgentBody.Move(dir * Time.deltaTime);
        });
    }

    //private void CheckImpactForce(CharacterBrain target)
    //{
    //    Vector3 vec = transform.position - target.transform.position;
    //    float force = target.characterAttack.Weight - characterAttack.PowerForce;
    //    if (force > 0)
    //        agent.AgentBody.Move(vec.normalized * force);
    //    else
    //        target.agent.AgentBody.Move(vec.normalized * force);
    //}
}
