using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : CharacterBrain, IPool
{
    [SerializeField] TypeEnemy typeEnemy;
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
    public virtual string objectName => typeEnemy.ToString();
    private void Awake()
    {
    }
    protected override void Start()
    {
        base.Start();
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
        SetupHPEnemy();
    }
    #region
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

    #endregion

    protected virtual void EnemyThinking(float TimeThink, int ratioRandomMove, UnityAction Random1, UnityAction random2)
    {
        if (enemyThinking || !Alive)
            return;
        enemyThinking = true;
        onFollowPlayer = false;
        randomMove = false;
        this.DelayCall(TimeThink, () =>
        {
            enemyThinking = false;
            int i = UnityEngine.Random.Range(0, 100);
            if (i < ratioRandomMove)
            {
                Random1?.Invoke();
                return;
            }
            random2?.Invoke();
        });
    }

    #region SetupEnemy
    public void SetLevelEnemy(LevelRomanNumerals level)
    {
        LevelEnemy = level;
    }
    private void SetupHPEnemy()
    {
        maxHealth = characterAttack.HP * (float)LevelEnemy;
        CurrentHealth = maxHealth;
        healthBar.SetHealh(maxHealth);
    }
    public float Distance()
    {
        return Vector3.Distance(transform.position, direction.transform.position);
    }
    protected override void EffectHit(Vector3 dir) { }
    #endregion

    #region EnemyAction
    public void SetOnEvent(bool value)
    {
        OnEvent = value;
    }
    public override void MoveTo(Vector3 direction)
    {
        base.MoveTo(direction);
    }
    public override void SetMoveWayPoint(Vector3 wayPoint, float time)
    {
        base.SetMoveWayPoint(wayPoint, time);
    }
    public override void Rolling()
    {
        throw new System.NotImplementedException();
    }
    protected override void OnAttackHit(CharacterBrain target)
    {
        target.TakeDamage(characterAttack.DamageEnemy);
        base.OnAttackHit(target);
    }
    public override void TakeDamage(float damage)
    {
        if (!Alive)
            return;
        if (!OnAction || enemyThinking)
        {
            string str = UnityEngine.Random.Range(1, 6).ToString();
            AudioManager.instance.Play("SoundEnemy" + str);
        }
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
        string str = UnityEngine.Random.Range(1, 4).ToString();
        AudioManager.instance.Play("EnemyDead" + str);
        InfomationPlayerManager.Instance.IncreaseValueOf(AttributeType.CountKillEnemy, 1);
        Vector3 dir = transform.position - direction.position;
        ImpactForce(dir.normalized * 20);
        deadBody.SetActive(true);
        healthBar.gameObject.SetActive(false);
        tranformOfAni.SetActive(false);
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
                ObjDrop objDropExp = objout as ObjDrop;
                objDropExp.Value = characterAttack.ExpEnemy * (float)LevelEnemy;
                GameLevelManager.Instance.CheckAllEnemyDead(this);
                GameLevelManager.Instance.RemoveSummonEnemy(this);
                Hide();
            });
        });
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
    protected override void StartAniAtk()
    {
        AudioManager.instance.Play("EnemyAtk");
        slash.transform.position = transform.position + GetDirection().normalized * characterAttack.AttackRange;
        base.StartAniAtk();
    }
    protected override void FinishAniAtk()
    {
        base.FinishAniAtk();
        agent.moveSpeed = characterAttack.NomalSpeed;
    }
    protected virtual void EventInDashAtks()
    {
        agent.moveSpeed = characterAttack.SpeedOnDash;
        SetMoveWayPoint(direction.transform.position, characterAttack.RangeDash);
    }
    protected void IsRandomMove()
    {
        randomMove = true;
        onFollowPlayer = false;
    }
    #endregion

    #region SpawnObjEnemy

    protected void SpawnObjBallFireLoop(int loop)
    {
        OnAction = true;
        int i = 0;
        float euler = 0;
        List<ObjectSkill> listObj = new List<ObjectSkill>();
        Quaternion test;
        this.WaitDelayCall(loop, 0.1f, () =>
        {
            test = Quaternion.Euler(0, euler, 0);
            Vector3 dir = test * transform.position;
            RewardSystem.Instance.SpawnObjectSkillEnemy("FireballsEnemy", transform.position + (dir.normalized * 2) + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            listObj.Add(outSkill);
            i++;
            euler += 45;
            if (i == loop)
            {
                euler = 0;
                for (int j = 0; j < listObj.Count; j++)
                {
                    test = Quaternion.Euler(0, euler, 0);
                    Vector3 direc = (test * (direction.position));
                    listObj[j].myTween = listObj[j].transform.DOMove(((GetDirection().normalized * 40) + direction.transform.position + (direc.normalized * 3)) + new Vector3(0, 1.5f, 0), 4f).OnComplete(() => { outSkill.Hide(); });
                    euler += 45;
                }
                AudioManager.instance.Play("FireBall8SkillEnemy");
                OnAction = false;
                FinishAniAtk();
            }
        });
    }
    protected void SpawnObjBallFire(int loop)
    {
        OnAction = true;
        int i = 0;
        float euler = 0;
        this.WaitDelayCall(loop, 0.5f, () =>
        {
            Quaternion test = Quaternion.Euler(0, euler, 0);
            Vector3 dir = test * transform.position;
            RewardSystem.Instance.SpawnObjectSkillEnemy("FireballsEnemy", transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            outSkill.myTween = outSkill.transform.DOMove(transform.position + (dir.normalized * 50) + new Vector3(0, 1.5f, 0), 4f).OnComplete(() => { outSkill.Hide(); });
            i++;
            euler += 45;
            if (i == loop)
            {
                OnAction = false;
                FinishAniAtk();
            }
        });
    }
    protected void SpawnObjBallFire()
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy("FireballsEnemy", transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.Init(1f, 4);
        outSkill.myTween = outSkill.transform.DOMove(transform.position + (GetDirection().normalized * 50) + new Vector3(0, 1.5f, 0), 4f).OnComplete(() => { outSkill.Hide(); });
    }
    protected void SpawnObjBallFireLoop(int loop, float eulerYs)
    {
        float eulerY = -eulerYs;
        for (int i = 0; i < loop; i++)
        {
            Quaternion test = Quaternion.Euler(0, eulerY, 0);
            Vector3 dir = test * GetDirection();
            RewardSystem.Instance.SpawnObjectSkillEnemy("FireballsEnemy", transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            outSkill.myTween = outSkill.transform.DOMove(transform.position + (dir.normalized * 50) + new Vector3(0, 1.5f, 0), 4f).OnComplete(() => { outSkill.Hide(); });
            eulerY += eulerYs;
        }

    }
    protected void SpawnObjBallJumpRandom()
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 vec = UnityEngine.Random.onUnitSphere * 25;
            RewardSystem.Instance.SpawnObjectSkillEnemy("FireballsEnemy", transform.position + new Vector3(0, 6, 0), out ObjectSkill outSkill);
            outSkill.Init(1f, 4);
            outSkill.myTween = outSkill.transform.DOJump(transform.position + new Vector3(vec.x, 0, vec.z), 10, 1, 3);
        }

    }
    protected void SpawnObjBoom()
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy("ObjBoomEnemy", transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.Init(1f, true);
        outSkill.transform.DOJump(direction.position, 3f, 1, 0.7f).OnComplete(() =>
        {
            outSkill.ActiveBoom();
        });
    }
    protected void SpawnObjBow()
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy("ArrowEnemy", transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.transform.rotation = Quaternion.LookRotation(GetDirection());
        outSkill.Init(1f, 1.8f);
        outSkill.myTween = outSkill.transform.DOMove(transform.position + (GetDirection().normalized * 60) + new Vector3(0, 1.5f, 0), 2f).OnComplete(() => { outSkill.Hide(); });
    }
    protected void SpawnObjBow(int loop, float eulerYs)
    {
        float eulerY = -eulerYs;
        for (int i = 0; i < loop; i++)
        {
            Quaternion test = Quaternion.Euler(0, eulerY, 0);
            Vector3 dir = test * GetDirection();
            RewardSystem.Instance.SpawnObjectSkillEnemy("ArrowEnemy", transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
            outSkill.transform.rotation = Quaternion.LookRotation(GetDirection());
            outSkill.Init(1f, 1.8f);
            outSkill.myTween = outSkill.transform.DOMove(transform.position + (dir.normalized * 60) + new Vector3(0, 1.5f, 0), 2f).OnComplete(() => { outSkill.Hide(); });
            eulerY += eulerYs;
        }
    }
    protected void SummonEnemyFromDead(List<ImpactableObjects> obj)
    {
        if (obj != null)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].Hide();
                GameLevelManager.Instance.SummonEnemy(obj[i].transform.position);
            }
        }
    }
    protected void SummonEnemy()
    {
        float eulerY = -60;
        for (int i = 0; i < 4; i++)
        {
            Quaternion test = Quaternion.Euler(0, eulerY, 0);
            Vector3 dir = test * GetDirection();
            GameLevelManager.Instance.SummonEnemy(transform.position + (dir.normalized * 3));
            eulerY += 30;
        }
    }
    protected void SpawnChain()
    {
        RewardSystem.Instance.SpawnObjectSkillEnemy("ObjRingDeadEnemy", direction.position, out ObjectSkill outSkill);
        outSkill.Init(1, true);
    }

    #endregion


    #region ObjectPooling
    public virtual void Show()
    {
        gameObject.SetActive(true);
        tranformOfAni.SetActive(true);
        agent.AgentBody.enabled = true;
        SetupHPEnemy();
        if (Distance() < playerDetectionRange)
        {
            randomMove = true;
            onTargetPlayer = true;
            onFollowPlayer = false;
        }
    }

    public void Hide()
    {
        deadBody.SetActive(false);
        gameObject.SetActive(false);
        slash.SetActiveSlash(false);
        onFollowPlayer = false;
        onTargetPlayer = false;
        OnDashAtk = false;
        onAniATK = false;
        OnAction = false;
        randomMove = true;
        enemyThinking = false;
        enemyRunFollow = false;
        ObjectPooling.Instance.PushToPoolEnemy(this);
    }
    public void OnReloadScence()
    {
        GameLevelManager.Instance.RemoveEnemy(this);
        GameLevelManager.Instance.RemoveSummonEnemy(this);
        Hide();
    }
    private void OnEnable()
    {
        ObseverConstants.ReloadScene.AddListener(OnReloadScence);
        ObseverConstants.OnBlackHeartBreak.AddListener(TakeDamage);
    }
    private void OnDisable()
    {
        ObseverConstants.ReloadScene.RemoveListener(OnReloadScence);
        ObseverConstants.OnBlackHeartBreak.RemoveListener(TakeDamage);
    }
    #endregion

}
