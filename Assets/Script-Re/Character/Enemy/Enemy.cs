using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Enemy : CharacterBrain , IPool
{
    [SerializeField] protected List<Vector3> wayPoints = null;
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 10f;
    [SerializeField] protected float DashAttackRange = 5f;
    [SerializeField] protected bool onFollowPlayer = false;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected GameObject deadBody;
    [SerializeField] protected bool OnDashAtk = false;
    [SerializeField] protected bool randomMove = true;
    [SerializeField] protected bool enemyThinking = false;
    [SerializeField] protected bool enemyRunFollow = false;

    public string objectName => gameObject.name;

    protected override void Start()
    {
        base.Start();
        characterAttack.Initialized();
        //wayPoints = GameManager.Instance.enemyWayPoints.Find(w => w.targetEnemy.Equals(Name))?.points.Select(p => p.position).ToList();
    }
    public virtual void Init()
    {
        if (direction == null)
        {
            direction = (Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform);
        }
    }
        
    protected virtual void Update()
    {
        
    }
    public void SetOnEvent(bool value)
    {
        OnEvent = value;
    }

    protected override void Rolling()
    {
        throw new System.NotImplementedException();
    }
    protected void ChangeFollowPlayer()
    {
        onFollowPlayer = !onFollowPlayer;
    }
    protected override void StartAniAtk()
    {
        base.StartAniAtk();
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
    }
    //protected void SpeedUP()
    //{
    //    agent.moveSpeed = 5.5f;
    //}
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(characterAttack.CurrentHit[0]);
        base.OnAttackHit(target);
    }
    public void Push()
    {
        if (Distance() <= characterAttack.AttackRange)
        {
            characterAnimator.SetTrigger("Attack");
        }
    }
    public float Distance()
    {
        return Vector3.Distance(transform.position, direction.transform.position);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        health -= damage;
        healthBar.SetActive();
        healthBar.UpdateHealth(health);
        if (health <= 0)
        {
            Dead();
        }
    }
    public override void Dead()
    {
        Vector3 dir = transform.position - direction.position;
        ImpactForce(dir.normalized * 20);
        deadBody.SetActive(true);
        healthBar.gameObject.SetActive(false);
        tranformOfAni.SetActive(false);
        slash.gameObject.SetActive(false);
        deadBody.transform.DOLocalMoveY(1.5f, 0.1f).OnComplete(() =>
        {
            deadBody.transform.DOLocalMoveY(0, 0.4f).OnComplete(() =>
            {
                enabled = false;
            });
        });
    }
    public override void EffectHit(Vector3 dir)
    {
        //Debug.Log(GameConstants.Slash);
        //AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }

    public void Show()
    {
        Debug.Log("Show");
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Hide");
        gameObject.SetActive(false);
    }
}
