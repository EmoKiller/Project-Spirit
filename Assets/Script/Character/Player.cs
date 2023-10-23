using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static CharacterAnimator;

public class Player : CharacterBrain
{
    protected override Vector3 direction => new Vector3(horizontal, 0, vertical);
    protected override bool Alive => throw new System.NotImplementedException();
    protected float horizontal => Input.GetAxis("Horizontal");
    protected float vertical => Input.GetAxis("Vertical");

    [SerializeField] Slash slash = null;
    
    public Transform PointTargetOfCamera;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        
    }
    protected void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            OnAttack();
        }
        if (horizontal != 0 || vertical!=0)
        {
            PointTargetOfCamera.position = new Vector3(horizontal, 0, vertical).normalized + transform.position;
            characterAnimator.SetFloat("horizontal", horizontal);
            characterAnimator.SetFloat("vertical", vertical);
            agent.MoveToDirection(direction);
        }
        else if (horizontal >= 0.2f || vertical >= 0.2f)
        {
            agent.MoveToDirection(direction.normalized);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Press E");
            EventDispatcher.TriggerEvent(Events.OnPlayerActionItems);
        }
    }
    
    public void OnAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            PointTargetOfCamera.position = transform.position + (raycastHit.point - transform.position).normalized;
            slash.transform.position = transform.position + (raycastHit.point - transform.position).normalized * 2f;
        }
        if (!characterAnimator.ataCanDo)
        {
            characterAnimator.ataCanDo = true;
            characterAnimator.SetTrigger("" + characterAnimator.combo);
        }
        
    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

}
