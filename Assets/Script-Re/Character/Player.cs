using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using static CharacterAnimator;

public class Player : CharacterBrain , IOrderable
{
    public static Player Instance = null;
    public enum Script
    {
        Player
    }
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject handCurses;
    [SerializeField] private SkillsPlayer skills;
    private float Horizontal => Input.GetAxis("Horizontal");
    private float Vertical => Input.GetAxis("Vertical");
    private int combo;
    private bool atkCanDo;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        skills = GetComponent<SkillsPlayer>();
    }
    protected override void Start()
    {
        base.Start();
        CurrentHealth = 999;
    }
    public void Init()
    {
        SetoffSlash();
        characterAnimator.AddStepAniAtk(SetOnSlash, SetoffSlash, StartCombo, FinishAniAtk);
        characterAnimator.AddStFishAni(StartAni, StopAni);
        EventDispatcher.Register(Script.Player, Events.PlayerDirection, () => direction);
        EventDispatcher.Register(Script.Player, Events.PlayerTransform, () => transform);
        EventDispatcher.Addlistener<string>(Script.Player, Events.PlayerTriggerAni, TriggerAni);
        EventDispatcher.Addlistener<Vector3, float>(Script.Player, Events.MoveToWaypoint, SetMoveWayPoint);
        EventDispatcher.Addlistener<Weapon>(Script.Player, Events.PlayerChangeWeapon, ChangeWeapon);
        EventDispatcher.Addlistener<CursesEquip>(Script.Player, Events.PlayerChangeCurses, ChangeCurses);
        EventDispatcher.Addlistener(Script.Player,Events.SetWeapon, SetWeapon);
        EventDispatcher.Addlistener<bool>(Script.Player,Events.SetOnEvent, SetEvent);
        EventDispatcher.Addlistener(Script.Player, Events.OnAttackHitEnemy, DelayTime);

        slash.AddActionAttack(OnAttackHit);
    }
    public void SetWeapon()
    {
        slash.AddActionAttack(OnAttackHit);
        characterAttack.Initialized(hand.GetComponentInChildren<Weapon>());
        slash.SetSizeBox(characterAttack.SlashBoxSize);
        Debug.Log(characterAttack.TotalDamage());
    }
    private void Update()
    {
        if (OnAction|| OnEvent || skills.OnUseSkill)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !atkCanDo && characterAttack.BoolWeaponEquip())
        {
            OnAttack();
            return;
        }
        if (onAniATK)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rolling();
            return;
        }
        if (Horizontal != 0 || Vertical!=0)
        {
            Rotation();
            direction.localPosition = new Vector3(Horizontal, 0, Vertical).normalized;
            characterAnimator.SetMovement(MovementType.Run, Vertical, Horizontal);
            characterAnimator.SetDirection(direction.transform.localPosition.x, direction.transform.localPosition.z);
            agent.MoveToDirection(new Vector3(Horizontal,0, Vertical));
            return;
        }
        characterAnimator.SetMovement(MovementType.Idle, Vertical, Horizontal);
    }
    public override void Rolling()
    {
        characterAnimator.SetTrigger(AnimationStates.Rolling);
        Vector3 dir = direction.position - transform.position;
        this.LoopDelayCall(0.3f, () =>
        {
            agent.MoveToDirection(dir * 3.5f);
            Rotation();
        });
    }
    public void LoopCondition(bool condition, Action callBack)
    {
        StartCoroutine(IELoopCondition(condition, callBack));
    }
    public IEnumerator IELoopCondition(bool condition, Action callBack)
    {
        while (condition)
        {
            callBack?.Invoke();
            
            //if (agent.AgentBody.radius > Vector3.Distance(transform.position, dirEnd))
            //{
            //    Debug.Log("condition filde");
                
            //    break;
            //}
            yield return null;

        }
        agent.AgentBody.isStopped = true;
        Debug.Log("Đã đến điểm đích!");
    }
    private void AttackRate()
    {
        characterAnimator.SetFloat("AttackRate", InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.AttackRate));
    }
    private void IncreasedMovementSpeed()
    {
        agent.moveSpeed *= InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.IncreasedMovementSpeed);
    }
    private void MovementSpeed()
    {
        agent.moveSpeed = InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.MovementSpeed);
    }
    
    protected override void OnAttackHit(CharacterBrain target)
    {
        float Crit = 1;
        CritHit(ref Crit);
        Debug.Log("PlayerHitEne" + " " + GetDamageCombo() * Crit);
        target.TakeDamage(GetDamageCombo() * Crit);
        base.OnAttackHit(target);
        EventDispatcher.Publish(Events.OnAttackHitEnemy);
    }
    private void DelayTime()
    {
        Time.timeScale = 0.6f;
        this.DelayCall(0.03f, () =>
        {
            Time.timeScale = 1;
        });
    }
    protected float CritHit(ref float value)
    {
        int i = UnityEngine.Random.Range(0, 100);
        if (i < InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CriticalHit))
            value *= 2;
        return value;
    }
    protected void ChanceOfHealing()
    {
        int i = UnityEngine.Random.Range(0, 100);
        if (i < InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.ChanceOfHealing))
        {
            //InfomationPlayerManager.Instance.AttributeOnChange(AttributeType.CurrentRedHeart,1);
        }
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        atkCanDo = false;
        combo = 0;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        EventDispatcher.Publish<float>(UIManager.Script.UIManager, Events.PlayerTakeDmg, damage);
    }
    public override void Dead()
    {
        Debug.Log("Player Dead");
    }
    protected override void EffectHit(Vector3 dir)
    {
        //Debug.Log(GameConstants.Slash);
        //AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }
    private void OnAttack()
    {
        atkCanDo = true;
        StartAniAtk();
        GameUtilities.ScreenRayCastOnWorld(DirSlash);
        Rotation();
        characterAnimator.SetComboAttack(combo);
        Vector3 vec = direction.position - transform.position;
        this.LoopDelayCall(0.1f, () =>
        {
            MoveTo(vec.normalized + transform.position);
            characterAnimator.SetFloat("horizontal", 0);
            characterAnimator.SetFloat("vertical", 0);
        });
    }
    public void DirSlash(Vector3 targetPos)
    {
        direction.position = transform.position + (targetPos - transform.position).normalized;
        slash.transform.position = transform.position + (targetPos - transform.position).normalized * 2f;
    }
    private void ChangeWeapon(Weapon weapon)
    {
        Weapon wp = hand.GetComponentInChildren<Weapon>();
        if (wp != null)
        {
            wp.transform.SetParent(null);
            wp.transform.ReSetEulerAngle();
        }
        Weapon obj = Instantiate(weapon, hand.transform);
        characterAttack.Initialized(obj);
        slash.SetSizeBox(characterAttack.SlashBoxSize);
    }
    private void ChangeCurses(CursesEquip curses)
    {
        CursesEquip cur = handCurses.GetComponentInChildren<CursesEquip>();
        if (cur != null)
        {
            cur.transform.SetParent(null);
            cur.transform.ReSetEulerAngle();
            SpriteRenderer spr = cur.GetComponent<SpriteRenderer>();
            spr.enabled = true;
        }
        CursesEquip obj = Instantiate(curses, handCurses.transform);
        InitCurses(obj);
    }
    private void InitCurses(CursesEquip curses)
    {
        skills.CrusesEquip = curses;
        SpriteRenderer spr = curses.GetComponent<SpriteRenderer>();
        spr.enabled = false;
    }
    public bool CheckAnimationStates(AnimationStates state)
    {
        return characterAnimator.CurrentAnimationState == state;
    }
    private float GetDamageCombo()
    {
        return characterAttack.CurrentHit[(int)characterAnimator.ComboATK];
    }
    private void StartCombo()
    {
        atkCanDo = false;
        if (combo < 3)
        {
            combo++;
        }
    }
}
