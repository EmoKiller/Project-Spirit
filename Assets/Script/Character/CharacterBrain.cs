using System;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected Slash slash = null;
    [SerializeField] protected Transform direction;
    [SerializeField] protected Transform tranformOfAni;
    [SerializeField] protected AnimationCurve forceCurve;
    [SerializeField] protected bool arried = false;
    [SerializeField] protected bool OnAction = false;
    protected Action action;
    //BaseCharacter
    protected string characterName {get; set;}
    protected float health { get; set; }
    protected float maxHealth { get; set; }
    [SerializeField] protected bool onAniAttck = false;
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
    public void MoveTo(Vector3 direction)
    {
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
        characterAnimator.SetFloat("horizontal", dir.normalized.x);
        characterAnimator.SetFloat("vertical", dir.normalized.z);
    }
    public virtual void SetMoveWayPoint(Transform wayPoint,float time)
    {
        arried = true;
        this.LoopDelayCall(time, () =>
        {
            MoveTo(wayPoint.position);
            Rotation();
        });
    }
    protected void Rotation()
    {
        Vector3 dir = direction.transform.position - transform.position;
        if (dir.normalized.x > 0)
            tranformOfAni.rotation = Quaternion.Euler(-10, 180, 0);
        else if (dir.normalized.x < 0)
            tranformOfAni.rotation = Quaternion.Euler(10, 0, 0);
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
    public virtual void TakeDamage(float damage) 
    {
        OnAction = true;
        onAniAttck = false;
        this.DelayCall(0.6f, () =>
        {
            OnAction = false;
        });
    }
    public abstract void EffectHit(Vector3 dir);
    public abstract void Dead(bool isDead);
    public void ImpactForce(Vector3 dir)
    {
        this.LoopDelayCall(0.3f, () =>
        {
            agent.AgentBody.Move(dir * Time.deltaTime);
        });
    }
    public void SetAction(Action action)
    {
        this.action = action;
        this.action?.Invoke();
    }
    public void SetTarget(Transform target)
    {
        direction = target;
    }
    public void SetArried(bool value)
    {
        arried = value;
    }
    public void SetStay()
    {
        arried = true;
        Rotation();
    }
    public void TriggerAni(string str)
    {
        characterAnimator.SetTrigger(str);
    }
}
