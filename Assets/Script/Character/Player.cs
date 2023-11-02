using DG.Tweening;
using UnityEngine;

public class Player : CharacterBrain
{
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
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, direction);
    }
    private void OnEnable()
    {
    }
    private void Init()
    {
        SetTypeSlash("Player");
        slash.SetSizeBox(4, 1, 4);
        SetoffSlash();
        characterAnimator.AddStepAni(SetOnSlash, SetoffSlash, StartCombo, FinishAni);
        slash.AddActionAttack(OnAttackHit);
        deadAction = Dead;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventDispatcher.Publish(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonDown);
            EventDispatcher.Publish(ListScript.IntroGame, Events.OpenBoxTalk);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            EventDispatcher.Publish(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonUp);
        }
        if (Input.GetKey(KeyCode.E))
        {
            EventDispatcher.Publish(ListScript.OnTringgerWaitAction, Events.OnTringgerActionItems);
        }

        if (Horizontal != 0 || Vertical!=0)
        {
            if (onAniAttck)
                return;
            Rotation();
            direction.position = new Vector3(Horizontal, 0, Vertical).normalized + transform.position;
            characterAnimator.SetFloat("horizontal", Horizontal);
            characterAnimator.SetFloat("vertical", Vertical);
            agent.MoveToDirection(new Vector3(Horizontal,0, Vertical));
        }
    }
    private void OnAttack()
    {
        atkCanDo = true;
        StartAni();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            direction.position = transform.position + (raycastHit.point - transform.position).normalized;
            slash.transform.position = transform.position + (raycastHit.point - transform.position).normalized * 2f;
        }
        Rotation();
        characterAnimator.SetFloat("horizontal", 0);
        characterAnimator.SetFloat("vertical", 0);
        //characterAnimator.SetFloat("Dir", direction.localPosition.x);
        characterAnimator.SetTrigger("" + combo);
        Vector3 vec = direction.position - transform.position;
        
        agent.AgentBody.Move(vec.normalized * 0);
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
    protected override void StartAni()
    {
        base.StartAni();
    }
    protected override void FinishAni()
    {
        base.FinishAni();
        atkCanDo = false;
        combo = 0;
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //Debug.Log("Player takeDamage" + damage);
    }
    public override void Dead(bool isDead)
    {
        throw new System.NotImplementedException();
    }

    public override void EffectHit(Vector3 dir)
    {
        AssetManager.Instance.InstantiateItems(AssetManager.Instance.SlashHit, transform, dir);
    }
    public Transform ReturnTrans()
    {
        return direction;
    }
}
