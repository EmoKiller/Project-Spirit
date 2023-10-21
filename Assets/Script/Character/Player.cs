using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Player : CharacterBrain
{
    
    //protected override CharacterBrain targetAttack => GameManager.Instance.enemies.Find(
    //    e => Vector3.Distance(transform.position, e.gameObject.transform.position) <= characterAttack.AttackRange);
    protected override Vector3 direction => new Vector3(horizontal, 0, vertical);
    protected override bool Alive => throw new System.NotImplementedException();
    protected float horizontal => Input.GetAxis("Horizontal");
    protected float vertical => Input.GetAxis("Vertical");

    [SerializeField] Slash slash = null;

    [SerializeField] private Transform PointTargetOfCamera
        ;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                //Debug.Log(raycastHit.point);
            }
            charactorDirectionMove.DirectionMove(transform.position, raycastHit.point, dirNum);
            PointTargetOfCamera.position = transform.position+ (raycastHit.point - transform.position).normalized;
            slash.transform.position = transform.position + (raycastHit.point - transform.position).normalized * 2;
        }
        if (horizontal != 0 || vertical!=0)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Run);
            PointTargetOfCamera.position = new Vector3(horizontal, 0, vertical).normalized + transform.position;
        }
        else
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        }
        agent.MoveToDirection(direction.normalized);
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            characterAnimator.SetTrigger("Slash1H");
            OnAttack();
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            //characterAnimator.SetTrigger("Slash2H");
            Debug.Log("");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Press E");
            
            //characterAttack.Initialized();
            //Debug.Log(characterAttack.Damage);
            
            //UIManager.Instance.AddSlider();
        }
        
    }
    private void LateUpdate()
    {
        charactorDirectionMove.DirectionMove(transform.position, PointTargetOfCamera.position, dirNum);
    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnEnemyAttack, OnHit);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveListener(Events.OnEnemyAttack, OnHit);
    }
    //public override void OnHit()
    //{
    //    Debug.Log("enemy Trigger OnHit");
    //    sliderHp.OnReduceValueChanged(targetAttack.CharacterAtk.Damage);
    //    sliderHp.gameObject.SetActive(true);
    //    if (!Alive)
    //    {
    //        Debug.Log("Enemy Dead");
    //    }


    //}
    public void OnAttack()
    {
        //if (CanAttack())
        //{
        //    EventDispatcher.TriggerEvent(Events.OnAttack);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }

}
