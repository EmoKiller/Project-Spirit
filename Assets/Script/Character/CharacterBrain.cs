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
    //BaseCharacter
    protected string characterName {get; set;}
    protected float health { get; set; }
    protected float maxHealth { get; set; }
    protected bool onAniAttck = false;
    protected Action<bool> dead = null;
    public bool Alive => health >= 0;
    public virtual string Name => characterName;
    
    protected virtual void Awake()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterAttack.Initialized();
        characterName = gameObject.name;
    }
    protected void SetTypeSlash()
    {
        slash.SetType(GetType().ToString());
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
            agent.AgentBody.Move(dir.normalized * force);
        else
            target.agent.AgentBody.Move(dir.normalized * force);
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
