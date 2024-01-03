using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected Slash slash = null;
    [SerializeField] protected Transform direction;
    [SerializeField] protected GameObject tranformOfAni;
    [SerializeField] private bool m_Action = false;
    [SerializeField] protected bool onAniATK = false;
    [SerializeField] protected bool OnEvent = false;
    protected bool OnAction 
    {
        get
        {
            return m_Action;
        }
        set
        {
            Set(value);
        }
    }
    
    //BaseCharacter
    private string characterName { get; set; }
    protected float health = 0;
    protected float maxHealth = 0;
    
    public bool Alive => health > 0;
    public virtual string Name => characterName;
    protected virtual void Start()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterName = gameObject.name;
        if (characterAttack.BoolWeaponEquip())
        {
            slash.SetSizeBox(characterAttack.SlashBoxSize);
        }
    }
    protected abstract void EffectHit(Vector3 dir);
    protected abstract void Dead();
    protected abstract void Rolling();
    protected virtual void SetOnSlash()
    {
        slash.SetActiveSlash(true);
    }
    protected virtual void SetoffSlash()
    {
        slash.SetActiveSlash(false);
    }
    protected virtual void StartAniAtk()
    {
        onAniATK = true;
    }
    protected virtual void FinishAniAtk()
    {
        characterAnimator.ResetTrigger();
        onAniATK = false;
    }
    protected void Rotation()
    {
        Vector3 dir = direction.transform.position - transform.position;
        if (dir.normalized.x > 0)
        {
            tranformOfAni.transform.localScale = new Vector3(-1, 1, 1);
            return;
        }
        tranformOfAni.transform.localScale = new Vector3(1, 1, 1);
    }
    protected virtual void OnAttackHit(CharacterBrain target)
    {
        if (!target.Alive)
            return;
        Vector3 dir = transform.position - target.transform.position;
        float force = target.characterAttack.Weight - characterAttack.PowerForce;
        if (force > 0)
        {
            ImpactForce(dir.normalized * force);
            return;
        }
        target.ImpactForce(dir.normalized * force);
    }
    public virtual void MoveTo(Vector3 direction)
    {
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
    }
    public virtual void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        float i = 0;
        this.LoopDelayCall(time, () =>
        {
            if (!Alive)
                return;
            MoveTo(wayPoint);
            Rotation();
            i++;
            if (i >= time-0.5f)
            {
                OnAction = false;
            }
        });
    }
    public virtual void TakeDamage(float damage) 
    {
        OnAction = true;
        onAniATK = false;
        this.DelayCall(0.2f, () =>
        {
            OnAction = false;
        });
    }
    public void ImpactForce(Vector3 dir)
    {
        this.LoopDelayCall(0.2f, () =>
        {
            agent.AgentBody.Move(dir * Time.deltaTime);
        });
    }
    public void SetAction(bool value)
    {
        OnAction = value;
    }
    public void SetEvent(bool value)
    {
        OnEvent = value;
    }
    public void SetStay()
    {
        OnAction = true;
        Rotation();
    }
    public void TriggerAni(string str)
    {
        characterAnimator.SetTrigger(str);
    }
    private void Set(bool value)
    {
        m_Action = value;
    }
}
