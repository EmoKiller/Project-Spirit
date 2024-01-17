using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
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
    public override bool Alive => InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CurrentRedHeart) > 0;
    private float Horizontal => Input.GetAxis("Horizontal");
    private float Vertical => Input.GetAxis("Vertical");
    private int combo;
    private bool atkCanDo;
    private bool canDropBoom = true;

    #region Init
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public void Init()
    {
        maxHealth = InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.MaxRedHeart);
        CurrentHealth = maxHealth;
        characterAnimator.AddStepAniAtk(SetOnSlash, SetoffSlash, StartCombo, FinishAniAtk);
        characterAnimator.AddStFishAni(StartAni, StopAni);
        EventDispatcher.Register(Script.Player, Events.PlayerDirection, () => direction);
        EventDispatcher.Register(Script.Player, Events.PlayerTransform, () => transform);
        EventDispatcher.Addlistener<string>(Script.Player, Events.PlayerTriggerAni, TriggerAni);
        EventDispatcher.Addlistener<Vector3, float>(Script.Player, Events.MoveToWaypoint, SetMoveWayPoint);
        EventDispatcher.Addlistener<Weapon>(Script.Player, Events.PlayerChangeWeapon, ChangeWeapon);
        EventDispatcher.Addlistener<CursesEquip>(Script.Player, Events.PlayerChangeCurses, ChangeCurses);
        EventDispatcher.Addlistener(Script.Player, Events.SetWeapon, SetWeapon);
        EventDispatcher.Addlistener<bool>(Script.Player, Events.SetOnEvent, SetEvent);
        EventDispatcher.Addlistener(Script.Player, Events.OnAttackHitEnemy, DelayTime);
        slash.AddActionAttack(OnAttackHit);
        ObseverConstants.OnAttributeValueChanged.AddListener(AttackRate);
        ObseverConstants.OnAttributeValueChanged.AddListener(IncreasedMovementSpeed);
    }
    #endregion

    #region Update

    private void Update()
    {
        if (!Alive ||OnAction|| OnEvent || skills.OnUseSkill)
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
    #endregion

    #region PlayerAction
    public override void Rolling()
    {
        characterAnimator.SetTrigger(AnimationStates.Rolling);
        Vector3 dir = direction.position - transform.position;
        this.LoopDelayCall(0.3f, () =>
        {
            agent.MoveToDirection(dir * 3.5f);
            Rotation();
        });
        DropBoom();
    }
    private void DropBoom()
    {
        if (InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.TheBomb) > 0 && canDropBoom == true)
        {
            canDropBoom = false;
            RewardSystem.Instance.SpawnObjectSkill(TypeEffectEnemy.ObjBoom.ToString(), transform.position, out ObjectSkill outSkill);
            outSkill.Init(InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.BombDamage), true);
            outSkill.ActiveBoom();
            this.DelayCall(7f, () => { canDropBoom = true; });
        }
    }
    protected override void OnAttackHit(CharacterBrain target)
    {
        if (!target.Alive)
            return;
        float Crit = 1;
        CritHit(ref Crit);
        target.TakeDamage(GetDamageCombo() * Crit);
        ChanceOfHealing();
        CompareImpactForce(target, GetForceCombo());
        EventDispatcher.Publish(Events.OnAttackHitEnemy);
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
            MoveTo((vec.normalized * GetMoveOnAttack()) + transform.position);
            characterAnimator.SetFloat("horizontal", 0);
            characterAnimator.SetFloat("vertical", 0);
        });
    }
    public void DirSlash(Vector3 targetPos)
    {
        direction.position = transform.position + (targetPos - transform.position).normalized;
        slash.transform.position = transform.position + (targetPos - transform.position).normalized * 2f;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        NegatingDamage(out bool negating);
        if (negating == true)
            return;
        EventDispatcher.Publish<float>(UIManager.Script.UIManager, Events.PlayerTakeDmg, damage);
    }
    public override void Dead()
    {
        Debug.Log("Player Dead");

    }
    protected override void EffectHit(Vector3 dir)
    {
    }
    private void StartCombo()
    {
        atkCanDo = false;
        if (combo < 3)
        {
            combo++;
        }
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        atkCanDo = false;
        combo = 0;
    }
    private void DelayTime()
    {
        Time.timeScale = 0.6f;
        this.DelayCall(0.03f, () =>
        {
            Time.timeScale = 1;
        });
    }
    #endregion

    #region Change Weapon/Curses
    public void SetWeapon()
    {
        slash.AddActionAttack(OnAttackHit);
        characterAttack.Initialized(hand.GetComponentInChildren<Weapon>());
        slash.SetSizeBox(characterAttack.SlashBoxSize);
        Debug.Log(characterAttack.TotalDamage());
    }
    private void ChangeWeapon(Weapon weapon)
    {
        Weapon wp = hand.GetComponentInChildren<Weapon>();
        if (wp != null)
        {
            wp.transform.SetParent(null);
            wp.transform.ReSetEulerAngle();
            wp.SetBoxCollider(true);
        }
        Weapon obj = Instantiate(weapon, hand.transform);
        characterAttack.Initialized(obj);
        slash.SetSizeBox(characterAttack.SlashBoxSize);
        obj.transform.localPosition = Vector3.zero;
        obj.SetBoxCollider(false);
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
            cur.SetBoxCollider(false);
        }
        CursesEquip obj = Instantiate(curses, handCurses.transform);
        InitCurses(obj);
        obj.SetBoxCollider(false);
    }
    private void InitCurses(CursesEquip curses)
    {
        skills.CrusesEquip = curses;
        SpriteRenderer spr = curses.GetComponent<SpriteRenderer>();
        spr.enabled = false;
    }
    #endregion

    #region Attribute Player
    
    private void AttackRate(AttributeType type, float newValue)
    {
        if (type != AttributeType.AttackRate)
            return;
        characterAnimator.SetFloat("AttackRate", InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.AttackRate));
    }
    private void IncreasedMovementSpeed(AttributeType type, float newValue)
    {
        if (type != AttributeType.IncreasedMovementSpeed)
            return;
        agent.moveSpeed *= InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.IncreasedMovementSpeed);
    }
    
    protected float CritHit(ref float value)
    {
        int i = UnityEngine.Random.Range(0, 100);
        if (i < InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CriticalHit))
            value *= 2;
        return value;
    }
    private void NegatingDamage(out bool NegatingDmg)
    {
        NegatingDmg = false;
        int i = UnityEngine.Random.Range(0, 100);
        if (i < InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.ChanceOfNegatingDamage))
            NegatingDmg = true;

    }
    protected void ChanceOfHealing()
    {
        int i = UnityEngine.Random.Range(0, 100);
        if (i < InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.ChanceOfHealing))
            InfomationPlayerManager.Instance.IncreaseValueOf(AttributeType.CurrentRedHeart, 1);
    }
    public bool CheckAnimationStates(AnimationStates state)
    {
        return characterAnimator.CurrentAnimationState == state;
    }
    private float GetDamageCombo()
    {
        return characterAttack.CurrentHit[(int)characterAnimator.ComboATK];
    }
    private float GetForceCombo() 
    {
        return characterAttack.ForceCombo[(int)characterAnimator.ComboATK];
    }
    private float GetMoveOnAttack()
    {
        return characterAttack.MoveOnAttack[(int)characterAnimator.ComboATK];
    }
   
    #endregion
}
