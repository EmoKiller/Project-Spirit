using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class CharacterBrain : MonoBehaviour , IDamageAble
{
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected Slash slash = null;
    [SerializeField] protected Transform direction;
    [SerializeField] protected GameObject tranformOfAni;

    

    [SerializeField] protected bool onAction = false;
    [SerializeField] protected bool onAniATK = false;
    [SerializeField] protected bool OnEvent = false;
    private string characterName { get; set; }
    //public
    public CharacterAnimator CharacterAni
    {
        get { return characterAnimator; }
    }
    public Transform Direction
    {
        get { return direction; }
    }
    public bool OnAction
    {
        get { return onAction; }
        set { onAction = value; }
    }
    public bool OnAniATK
    {
        get { return onAniATK; }
    }
    
    public bool Alive => CurrentHealth > 0;
    public string Name => characterName;

    

    protected virtual void Start()
    {
        agent.Initialized();
        characterName = gameObject.name;
        if (characterAttack.BoolWeaponEquip())
        {
            slash.SetSizeBox(characterAttack.SlashBoxSize);
        }
    }



    #region Health / Die

    public float CurrentHealth { get; set; }
    public float maxHealth { get; set; }
    public virtual void TakeDamage(float damage)
    {
        OnAction = true;
        onAniATK = false;
        this.DelayCall(0.2f, () =>
        {
            OnAction = false;
        });
    }
    public virtual void Dead()
    {

    }
    #endregion

    protected abstract void EffectHit(Vector3 dir);
    
    public abstract void Rolling();
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
        CompareImpactForce(target, characterAttack.PowerForce);
    }
    protected void CompareImpactForce(CharacterBrain target,float PowerForce)
    {
        Vector3 dir = transform.position - target.transform.position;
        float force = target.characterAttack.Weight - PowerForce;
        if (force > 0)
        {
            ImpactForce(dir.normalized * force);
            return;
        }
        target.ImpactForce(dir.normalized * force);
    }
    protected void ImpactForce(Vector3 dir)
    {
        this.LoopDelayCall(0.2f, () =>
        {
            agent.AgentBody.Move(dir * Time.deltaTime);
        });
    }
    public virtual void MoveTo(Vector3 direction)
    {
        Vector3 dir = direction - transform.position;
        agent.MoveToDirection(dir.normalized);
    }
    //public virtual void SetMoveWayPoints(Vector3 wayPoint)
    //{
    //    float i = 0;
    //    this.LoopCondition(Vector3.Distance(transform.position, wayPoint) > 0.1f , () =>
    //    {
    //        if (!Alive)
    //            return;
    //        MoveTo(wayPoint);
    //        Rotation();
    //        i++;
    //        //if (i >= time - 0.5f)
    //        //{
    //        //    OnAction = false;
    //        //}
    //    });
    //}
    public virtual void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        float i = 0;
        Rotation();
        this.LoopDelayCall(time, () =>
        {
            if (!Alive)
                return;
            MoveTo(wayPoint);
            i++;
            if (i >= time - 0.5f)
            {
                OnAction = false;
            }
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
    public void StartAni()
    {
        OnAction = true;
    }
    public void StopAni()
    {
        OnAction = false;
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
    public Vector3 GetDirection()
    {
        Vector3 dir = direction.position - transform.position;
        return dir;
    }

    
}
