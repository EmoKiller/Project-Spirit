using DG.Tweening;
using System;
using UnityEngine;

public class Player : CharacterBrain , IOrderable
{
    public enum Script
    {
        Player
    }
    [SerializeField] private GameObject hand;
    private float Horizontal => Input.GetAxis("Horizontal");
    private float Vertical => Input.GetAxis("Vertical");
    private int combo;
    private bool atkCanDo;
    protected override void Start()
    {
        base.Start();
    }
    public void Init()
    {
        SetoffSlash();
        characterAnimator.AddStepAniAtk(SetOnSlash, SetoffSlash, StartCombo, FinishAniAtk);
        characterAnimator.AddStFishAni(StartAni, StopAni);
        slash.AddActionAttack(OnAttackHit);
        characterAttack.Initialized(hand.GetComponentInChildren<Weapon>());
        slash.SetSizeBox(characterAttack.SlashBoxSize);
        EventDispatcher.Register(Script.Player, Events.PlayerDirection, () => direction);
        EventDispatcher.Register(Script.Player, Events.PlayerTransform, () => transform);
        EventDispatcher.Addlistener<string>(Script.Player, Events.PlayerTriggerAni, TriggerAni);
        EventDispatcher.Addlistener<Vector3, float>(Script.Player, Events.MoveToWaypoint, SetMoveWayPoint);
        EventDispatcher.Addlistener<Weapon>(Script.Player, Events.PlayerChangeWeapon, ChangeWeapon);
    }
    private void Update()
    {
        if (OnAction)
            return;
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
            //characterAnimator.SetTrigger("Rolling");

            characterAnimator.SetRolling(CharacterAnimator.AnimationState.Rolling);
            return;
        }
        if (Horizontal != 0 || Vertical!=0)
        {
            Rotation();
            direction.localPosition = new Vector3(Horizontal, 0, Vertical).normalized;
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Run, Vertical, Horizontal);
            characterAnimator.SetDirection(direction.transform.localPosition.x, direction.transform.localPosition.z);
            agent.MoveToDirection(new Vector3(Horizontal,0, Vertical));
            return;
        }
        characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle, Vertical, Horizontal);
    }
    protected override void Rolling()
    {
        Vector3 dir = direction.position - transform.position;
        this.LoopDelayCall(0.3f, () =>
        {
            agent.MoveToDirection(dir * 3.5f);
            Rotation();
        });
    }
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(GetDamageCombo());
        base.OnAttackHit(target);
    }
    protected override void StartAniAtk()
    {
        base.StartAniAtk();
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        atkCanDo = false;
        combo = 0;
    }

    public override void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        characterAnimator.SetMovement(CharacterAnimator.MovementType.Run, Vertical, Horizontal);
        Vector3 dir = wayPoint - transform.position;
        direction.position = dir.normalized + transform.position;
        base.SetMoveWayPoint(wayPoint, time);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        EventDispatcher.Publish(UiControllerHearts.Script.UiControllerHearts, Events.PlayerTakeDamage);
    }
    public override void Dead()
    {
        Debug.Log("Player Dead");
    }
    public void StartAni()
    {
        OnAction = true;
    }
    public void StopAni()
    {
        OnAction = false;
    }
    public override void EffectHit(Vector3 dir)
    {
        //Debug.Log(GameConstants.Slash);
        //AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }
    private void OnAttack()
    {
        atkCanDo = true;
        StartAniAtk();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            direction.position = transform.position + (raycastHit.point - transform.position).normalized;
            slash.transform.position = transform.position + (raycastHit.point - transform.position).normalized * 2f;
        }
        Rotation();
        //characterAnimator.SetTrigger("" + combo);
        characterAnimator.SetComboAttack(combo);
        Vector3 vec = direction.position - transform.position;
        this.LoopDelayCall(0.1f, () =>
        {
            MoveTo(vec.normalized + transform.position);
            characterAnimator.SetFloat("horizontal", 0);
            characterAnimator.SetFloat("vertical", 0);
        });
    }
    private void ChangeWeapon(Weapon weapon)
    {
        Weapon wp = hand.GetComponentInChildren<Weapon>();
        if (wp != null)
        {
            wp.transform.SetParent(null);
            wp.transform.ReSetTransform();
        }
        Weapon obj = Instantiate(weapon, hand.transform);
        characterAttack.Initialized(obj);
    }
    private float GetDamageCombo()
    {
        return characterAttack.CurrentHit[int.Parse(characterAnimator.currentTrigger)];
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
