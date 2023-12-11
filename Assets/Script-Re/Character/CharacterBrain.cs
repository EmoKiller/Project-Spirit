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
    protected float health { get; set; }
    protected float maxHealth { get; set; }
    [SerializeField] protected bool onAniATK = false;
    public bool Alive => health > 0;
    public virtual string Name => characterName;
    protected virtual void Start()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterAttack.Initialized();
        characterName = gameObject.name;
        if (characterAttack.BoolWeaponEquip())
        {
            slash.SetSizeBox(characterAttack.SlashBoxSize);
        }
    }
    public abstract void EffectHit(Vector3 dir);
    public abstract void Dead();
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
        Vector3 dir = transform.position - target.transform.position;
        float force = target.characterAttack.Weight - characterAttack.PowerForce;
        if (force > 0)
        {
            ImpactForce(dir.normalized * force);
            return;
        }
        target.ImpactForce(dir.normalized * force);
    }
    public void MoveTo(Vector3 direction)
    {
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
        characterAnimator.SetFloat("horizontal", dir.normalized.x);
        characterAnimator.SetFloat("vertical", dir.normalized.z);
    }
    public virtual void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        this.LoopDelayCall(time, () =>
        {
            MoveTo(wayPoint);
            Rotation();
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
