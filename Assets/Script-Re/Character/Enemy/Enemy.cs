using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : CharacterBrain , IPool
{
    public LevelRomanNumerals LevelEnemy;
    [SerializeField] protected List<Vector3> wayPoints = null;
    [SerializeField] protected int currentWaypointIndex = 0;
    [SerializeField] protected float playerDetectionRange = 10f;
    [SerializeField] protected float DashAttackRange = 6f;
    [SerializeField] protected bool onFollowPlayer = false;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected GameObject deadBody;
    [SerializeField] protected bool OnDashAtk = false;
    [SerializeField] protected bool randomMove = true;
    [SerializeField] protected bool enemyThinking = false;
    [SerializeField] protected bool enemyRunFollow = false;

    public virtual string objectName => gameObject.name;

    protected override void Start()
    {
        base.Start();
        characterAttack.Initialized();
        slash.SetSizeBox(characterAttack.SlashBoxSize);
        maxHealth = characterAttack.HP * (float)LevelEnemy;
        health = maxHealth;
        healthBar.SetHealh(maxHealth);
    }
    public virtual void Init()
    {
        if (direction == null)
        {
            direction = (Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform);
        }
        maxHealth = characterAttack.HP * (float)LevelEnemy;
        health = maxHealth;
        
        healthBar.SetHealh(maxHealth);
        slash.AddActionAttack(OnAttackHit);
    }
        
    protected virtual void Update()
    {
        
    }
    public void SetLevelEnemy(LevelRomanNumerals level)
    {
        LevelEnemy = level;
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
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(characterAttack.DamageEnemy);
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
    protected override void Dead()
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
                agent.AgentBody.enabled = false;
                if(SceneManager.GetActiveScene().name == "IntroGame")
                {
                    RewardSystem.Instance.DropImpactableObjects(deadBody.gameObject.name, transform.position);
                    gameObject.SetActive(false);
                    return;
                }
                Hide();
                RewardSystem.Instance.DropImpactableObjects(deadBody.gameObject.name, transform.position);
                RewardSystem.Instance.DropObject(TypeItemsCanDrop.ObjDropExp, transform.position, out ObjectDropOnWorld objout);
                ObjDropExp objDropExp = objout as ObjDropExp;
                objDropExp.NumEXP = characterAttack.ExpEnemy * (float)LevelEnemy;

            });
        });
    }
    protected override void EffectHit(Vector3 dir)
    {
        //Debug.Log(GameConstants.Slash);
        //AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        GameLevelManager.Instance.RemoveinList(this);
        ObjectPooling.Instance.PushToPoolEnemy(this);
        deadBody.SetActive(false);
        gameObject.SetActive(false);
    }
}
