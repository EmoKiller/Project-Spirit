using DG.Tweening;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : CharacterBrain , IPool
{
    public enum Script
    {
        Enemy
    }
    public LevelRomanNumerals LevelEnemy;
    [SerializeField] protected float playerDetectionRange = 25f;
    [SerializeField] protected float DashAttackRange = 6f;
    [SerializeField] protected bool onFollowPlayer = false;
    [SerializeField] protected bool onTargetPlayer = false;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected GameObject deadBody;
    [SerializeField] protected bool OnDashAtk = false;
    [SerializeField] protected bool randomMove = true;
    [SerializeField] protected bool enemyThinking = false;
    [SerializeField] protected bool enemyRunFollow = false;
    public override bool Alive => CurrentHealth > 0;
    public virtual string objectName => gameObject.name;
    private void Awake()
    {
        
    }
    protected override void Start()
    {
        base.Start();
        characterAttack.Initialized();
        slash.SetSizeBox(characterAttack.SlashBoxSize);
        slash.AddActionAttack(OnAttackHit);
        maxHealth = characterAttack.HP * (float)LevelEnemy;
        CurrentHealth = maxHealth;
        healthBar.SetHealh(maxHealth);
        
    }
    public virtual void Init()
    {
        if (direction == null)
            direction = (Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform);
        maxHealth = characterAttack.HP * (float)LevelEnemy;
        CurrentHealth = maxHealth;
        healthBar.SetHealh(maxHealth);
        ObseverConstants.OnBlackHeartBreak.AddListener(TakeDamage);
    }
        
    protected virtual void Update()
    {
        //if (!Alive || OnAction)
        //    return;
        //switch (characterAnimator.CurrentAnimationState)
        //{
        //    case CharacterAnimator.AnimationStates.Idle:
        //        EnemyIdle();
        //        break;
        //    case CharacterAnimator.AnimationStates.Movement:
        //        EnemyMovement();
        //        break;
        //    case CharacterAnimator.AnimationStates.RandomMove:
        //        EnemyRandomMove();
        //        break;
        //    case CharacterAnimator.AnimationStates.Chase:
        //        EnemyChase();
        //        break;
        //    case CharacterAnimator.AnimationStates.Attack:
        //        EnemyAttack();
        //        break;
        //    case CharacterAnimator.AnimationStates.RunAtk:
        //        EnemyRunAtk();
        //        break;
        //    case CharacterAnimator.AnimationStates.Rolling:
        //        EnemyRolling();
        //        break;
        //    case CharacterAnimator.AnimationStates.RangeAttack:
        //        EnemyRangeAtk();
        //        break;
        //    case CharacterAnimator.AnimationStates.UseSkill:
        //        EnemyUseSkill();
        //        break;
        //    case CharacterAnimator.AnimationStates.MoveToTarget:
        //        EnemyMoveToTarget();
        //        break;
        //}
        //CheckTransition();
    }
    //protected virtual void EnemyIdle()
    //{
        
    //}
    //protected virtual void EnemyMovement()
    //{

    //}
    //protected virtual void EnemyMoveToTarget()
    //{
    //    MoveTo(direction.transform.position);
    //    Rotation();
    //    if (Distance() < playerDetectionRange)
    //        characterAnimator.SetAnimationState(CharacterAnimator.AnimationStates.RandomMove);
    //}
    //protected virtual void EnemyRandomMove()
    //{
    //    if (randomMove == true)
    //        return;

    //    randomMove = true;
    //    Vector3 vec = Random.onUnitSphere;
    //    Vector3 point = vec.normalized * 15 + direction.transform.position;
    //    SetMoveWayPoint(point, 4);
    //}
    //protected virtual void EnemyChase()
    //{

    //}
    //protected virtual void EnemyAttack()
    //{

    //}
    //protected virtual void EnemyRunAtk()
    //{

    //}
    //protected virtual void EnemyRolling()
    //{

    //}
    //protected virtual void EnemyRangeAtk()
    //{

    //}
    //protected virtual void EnemyUseSkill()
    //{

    //}
    //protected virtual void CheckTransition()
    //{
    //    if (Distance() > playerDetectionRange)
    //        characterAnimator.SetAnimationState(CharacterAnimator.AnimationStates.MoveToTarget);

    //}
    protected virtual void EnemyThinking(float TimeThink, int ratioRandomMove , UnityAction Random1, UnityAction random2)
    {
        if (enemyThinking || !Alive)
            return;
        enemyThinking = true;
        onFollowPlayer = false;
        randomMove = false;
        this.DelayCall(TimeThink, () =>
        {
            enemyThinking = false;
            int i = Random.Range(0, 100);
            if (i < ratioRandomMove)
            {
                Random1?.Invoke();
                return;
            }
            random2?.Invoke();
        });
    }




    public void SetLevelEnemy(LevelRomanNumerals level)
    {
        LevelEnemy = level;
    }
    public void SetOnEvent(bool value)
    {
        OnEvent = value;
    }

    public override void Rolling()
    {
        throw new System.NotImplementedException();
    }
    protected void ChangeFollowPlayer()
    {
        onFollowPlayer = !onFollowPlayer;
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
    protected virtual void DashAtk()
    {
        OnDashAtk = true;
        characterAnimator.SetTrigger("DashAttack");
    }
    protected virtual void EventInDashAtks()
    {
        agent.moveSpeed = 14;
        SetMoveWayPoint(direction.transform.position * 3, 3.5f);
    }
    public override void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        base.SetMoveWayPoint(wayPoint, time);
        //this.DelayCall(time, () =>
        //{
        //    characterAnimator.SetTrigger("Idie");
        //});
    }
    public override void MoveTo(Vector3 direction)
    {
        base.MoveTo(direction);
    }
    public float Distance()
    {
        return Vector3.Distance(transform.position, direction.transform.position);
    }
    public override void TakeDamage(float damage)
    {
        if (!Alive)
            return;
        base.TakeDamage(damage);
        CurrentHealth -= damage;
        healthBar.SetActive();
        healthBar.UpdateHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Dead();
        }
    }
    public override void Dead()
    {
        InfomationPlayerManager.Instance.IncreaseValueOf(AttributeType.CountKillEnemy, 1);
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
                if (deadBody != null)
                {
                    if (SceneManager.GetActiveScene().name == "IntroGame")
                    {
                        RewardSystem.Instance.DropImpactableObjects(deadBody.gameObject.name, transform.position);
                        gameObject.SetActive(false);
                        return;
                    }
                    RewardSystem.Instance.DropImpactableObjects(deadBody.gameObject.name, transform.position);
                }
                RewardSystem.Instance.DropObject(TypeItemsCanDrop.ObjDropExp, transform.position, out ObjectDropOnWorld objout);
                ObjDropExp objDropExp = objout as ObjDropExp;
                objDropExp.NumEXP = characterAttack.ExpEnemy * (float)LevelEnemy;
                Hide();
            });
        });
    }
    protected override void EffectHit(Vector3 dir)
    {
        //Debug.Log(GameConstants.Slash);
        //AssetManager.Instance.InstantiateItems(string.Format(GameConstants.Slash, "HitFX_0.prefab"), transform, dir);
    }
    
    protected virtual void SpawnObject(string name, Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy(name, transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.Init(1f, 15);
        outSkill.transform.DOMove(transform.position + (foward * 30) + new Vector3(0, 1.5f, 0), 10);
    }
    #region ObjectPooling
    public void Show()
    {
        gameObject.SetActive(true);
        if (Distance() < playerDetectionRange)
        {
            randomMove = true;
            onTargetPlayer = true;
            onFollowPlayer = false;
        }
    }

    public void Hide()
    {
        onFollowPlayer = false;
        onTargetPlayer = false;
        OnDashAtk = false;
        randomMove = false;
        enemyThinking = false;
        enemyRunFollow = false;
        ObseverConstants.OnBlackHeartBreak.RemoveListener(TakeDamage);
        GameLevelManager.Instance.RemoveInList(this);
        ObjectPooling.Instance.PushToPoolEnemy(this);
        deadBody.SetActive(false);
        gameObject.SetActive(false);
    }
    #endregion

}
