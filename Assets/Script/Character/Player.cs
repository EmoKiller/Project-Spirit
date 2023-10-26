using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : CharacterBrain
{
    protected float Horizontal => Input.GetAxis("Horizontal");
    protected float Vertical => Input.GetAxis("Vertical");
    public Action<Enemy> enemy;
    //private Transform direction;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        enemy = AttackOnEnemy;
        slash.SetSizeBox(4,1,4);
    }
    protected void Update()
    {
        if (Input.GetMouseButtonDown(0) && !characterAnimator.ataCanDo)
        {
            OnAttack();
        }
        if (Horizontal != 0 || Vertical!=0)
        {
            //direction.position = new Vector3(horizontal, 0, vertical).normalized + transform.position;
            characterAnimator.SetFloat("horizontal", Horizontal);
            characterAnimator.SetFloat("vertical", Vertical);
            agent.MoveToDirection(new Vector3(Horizontal,0, Vertical));
        }
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Press E");
            EventDispatcher.TriggerEvent(Events.OnPlayerActionItems);
        }
    }
    private void OnAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //direction.position = transform.position + (raycastHit.point - transform.position).normalized;
            slash.transform.position = transform.position + (raycastHit.point - transform.position).normalized * 2f;
        }
        //characterAnimator.SetFloat("Dir", direction.localPosition.x);
        characterAnimator.SetTrigger("" + characterAnimator.combo);
        characterAnimator.ataCanDo = true;
        //Vector3 vec = direction.position - transform.position;

        //agent.agentBody.Move(vec.normalized);
        slash.gameObject.SetActive(true);
    }
    private void AttackOnEnemy(Enemy ene)
    {
        ene.TakeDamage(GetDamageCombo());
        Vector3 vec = transform.position - ene.transform.position;
        //float force = ene.characterAttack.Weight - characterAttack.PowerForce;
        //if (force > 0)
        //{
        //    agent.agentBody.Move(vec.normalized * force);
        //}
        //else
        //{
        //    ene.agent.agentBody.Move(vec.normalized * force);
        //}
        //EventDispatcher.TriggerEvent(Events.OnEnemyHit);
        //if (!ene.Alive)
        //{
        //    EventDispatcher.TriggerEvent(Events.OnEnemyDead);
        //}
    }

    private float GetDamageCombo()
    {
        return characterAttack.CurrentHit[int.Parse(characterAnimator.currentTrigger)];
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
    public override void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }

    public override void StartAttack(float damage)
    {
        throw new NotImplementedException();
    }
}
