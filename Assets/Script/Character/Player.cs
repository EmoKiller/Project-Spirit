using UnityEngine;

public class Player : CharacterBrain
{
    [SerializeField]private GameObject hand;
    private float Horizontal => Input.GetAxis("Horizontal");
    private float Vertical => Input.GetAxis("Vertical");
    private int combo;
    private bool atkCanDo;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        Init();
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransformPlayer, direction);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.ReturnTargetPlayer);
        EventDispatcher.Addlistener(ListScript.Player,Events.TriggerAction, Detention);
        EventDispatcher.Addlistener<string>(ListScript.Player, Events.TriggerAni, TriggerAni);
        EventDispatcher.Addlistener<Transform,float>(ListScript.Player, Events.MoveTo, SetMoveWayPoint);
        EventDispatcher.Addlistener<Weapon>(ListScript.Player,Events.ChangeWeapon, ChangeWeapon);
    }
    private void OnEnable()
    {
    }
    private void Init()
    {
        SetTypeSlash("Player");
        slash.SetSizeBox(4, 1, 4);
        SetoffSlash();
        characterAnimator.AddStepAniAtk(SetOnSlash, SetoffSlash, StartCombo, FinishAniAtk);
        characterAnimator.AddStFishAni(StartAni,StopAni);
        slash.AddActionAttack(OnAttackHit);
        deadAction = Dead;
        characterAttack.Initialized(hand.GetComponentInChildren<Weapon>());
    }
    private void Update()
    {
        if (OnAction)
            return;
        if (Input.GetMouseButtonDown(0) && !atkCanDo)
        {
            OnAttack();
            return;
        }
        if (Horizontal != 0 || Vertical!=0)
        {
            if (onAniAttck)
                return;
            Rotation();
            direction.localPosition = new Vector3(Horizontal, 0, Vertical).normalized;
            characterAnimator.SetFloat("vertical", Vertical);
            characterAnimator.SetFloat("horizontal", Horizontal);
            characterAnimator.SetFloat("UpDown", direction.transform.localPosition.z);
            agent.MoveToDirection(new Vector3(Horizontal,0, Vertical));
        }
    }
    
    private void ChangeWeapon(Weapon weapon)
    {
        Weapon wp = hand.GetComponentInChildren<Weapon>();
        if (wp != null)
        {
            Instantiate(wp,transform.position,transform.rotation);
            Destroy(wp.gameObject);
        }
        Instantiate(weapon, hand.transform);

    }
    public override void SetMoveWayPoint(Transform wayPoint, float time)
    {
        Vector3 dir = wayPoint.position - transform.position;
        direction.position = dir.normalized + transform.position;
        base.SetMoveWayPoint(wayPoint, time);
    }
    private void Detention()
    {
        characterAnimator.SetTrigger("Detention");
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
        characterAnimator.SetTrigger("" + combo);
        Vector3 vec = direction.position - transform.position;
        this.LoopDelayCall(0.1f, () =>
        {
            MoveTo(vec.normalized + transform.position);
            characterAnimator.SetFloat("horizontal", 0);
            characterAnimator.SetFloat("vertical", 0);
        });
    }
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(GetDamageCombo());
        base.OnAttackHit(target);
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
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //Debug.Log("Player takeDamage" + damage);
    }
    public override void Dead()
    {
        Debug.Log("Player Dead");
        //throw new System.NotImplementedException();
        
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
        Debug.Log(GameConstants.Slash);
        AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }
    public Transform ReturnTrans()
    {
        return direction;
    }
}
