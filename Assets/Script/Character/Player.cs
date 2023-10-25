using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : CharacterBrain
{
    protected override Vector3 direction => new Vector3(horizontal, 0, vertical);
    public override bool Alive => throw new System.NotImplementedException();
    protected float horizontal => Input.GetAxis("Horizontal");
    protected float vertical => Input.GetAxis("Vertical");

    [SerializeField] Slash slash = null;
    public static Action<Enemy> enemy;
    public Transform PointTargetOfCamera;
    protected override void Awake()
    {
        base.Awake();
        slash.gameObject.SetActive(false);
    }
    private void Start()
    {
        enemy = AttackOnEnemy;
    }
    protected void Update()
    {
        if (Input.GetMouseButtonDown(0) && !characterAnimator.ataCanDo)
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
        if (Input.GetKey(KeyCode.E))
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
        characterAnimator.SetFloat("Dir", PointTargetOfCamera.localPosition.x);
        characterAnimator.ataCanDo = true;
        slash.gameObject.SetActive(true);

        characterAnimator.SetTrigger("" + characterAnimator.combo);


        Vector3 vec = PointTargetOfCamera.position - transform.position;
        
        agent.agentBody.Move(vec.normalized);
        //transform.DOMove(transform.position + vec.normalized * 0.8f, 0.3f);
    }
    private void AttackOnEnemy(Enemy ene)
    {
        ene.sliderHp.gameObject.SetActive(true);
        ene.sliderHp.OnReduceValueChanged(characterAttack.CurrentHit[int.Parse(characterAnimator.currentTrigger)]);
        Vector3 vec = transform.position - ene.transform.position;
        float force = ene.characterAttack.Weight - characterAttack.PowerForce;
        if (force > 0)
        {
            agent.agentBody.Move(vec.normalized*force);
        }
        else
        {
            ene.agent.agentBody.Move(vec.normalized * force);
        }
        EventDispatcher.TriggerEvent(Events.OnEnemyHit);
        if (!ene.Alive)
        {
            EventDispatcher.TriggerEvent(Events.OnEnemyDead);
        }
    }
    private void SlashObj()
    {
        slash.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnRemoveSlash, SlashObj);
    }
    private void OnDisable()
    {
        enemy = null;
        EventDispatcher.RemoveListener(Events.OnRemoveSlash, SlashObj);
    }

}
