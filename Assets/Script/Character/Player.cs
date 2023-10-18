using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Player : CharacterBrain
{
    
    protected override CharacterBrain targetAttack => GameManager.Instance.enemies.Find(
        e => Vector3.Distance(transform.position, e.gameObject.transform.position) <= characterAttack.AttackRange);
    protected override Vector3 direction => new Vector3(horizontal, 0, vertical);
    protected override bool Alive => throw new System.NotImplementedException();
    [SerializeField] protected ChildrenSlider sliderHp;


    protected float horizontal => Input.GetAxis("Horizontal");
    protected float vertical => Input.GetAxis("Vertical");
    [SerializeField] private Transform PointChecklr;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        agent.moveSpeed = 4f;
    }
    protected void Update()
    {
        agent.MoveToDirection(direction.normalized);
        charactorDirectionMove.DirectionMove(transform.position, transform.position + direction, dirNum);
        
        
        if (horizontal != 0 || vertical!=0)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Run);
            PointChecklr.position = new Vector3(horizontal, 0, 0).normalized + transform.position;
        }
        else
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            characterAnimator.SetTrigger("Slash1H");
            OnAttack();
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            //characterAnimator.SetTrigger("Slash2H");
            Debug.Log(targetAttack);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Press E");
            
            //characterAttack.Initialized();
            //Debug.Log(characterAttack.Damage);
            
            //UIManager.Instance.AddSlider();
        }
        
    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnEnemyAttack, OnHit);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveListener(Events.OnEnemyAttack, OnHit);
    }
    public override void OnHit()
    {
        Debug.Log("enemy Trigger OnHit");
        sliderHp.OnReduceValueChanged(targetAttack.CharacterAtk.Damage);
        sliderHp.gameObject.SetActive(true);
        if (!Alive)
        {
            Debug.Log("Enemy Dead");
        }


    }
    public void OnAttack()
    {
        if (CanAttack())
        {
            EventDispatcher.TriggerEvent(Events.OnAttack);
        }
    }
    
}
