using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected string characterName = "";
    [SerializeField] protected float health = 1;
    [SerializeField] protected float currentHealth = 1;
    
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected ManagerDirectionMove charactorDirectionMove = null;
    protected abstract CharacterBrain targetAttack { get; }
    protected abstract Vector3 direction { get; }
    protected int dirNum = 0;

    public virtual float Health => health;
    public virtual float CurrentHealth => currentHealth;
    public string Name => characterName;

    public bool Alive => currentHealth > 0;
    public MeshAgent Agent => agent;
    public CharacterAttack CharacterAtk => characterAttack;

    protected virtual void Awake()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        charactorDirectionMove.Initialized();
        characterName = gameObject.name;
        charactorDirectionMove.SetActiveDirectionMove(DirectionMove.Front);
    }
    private void Start()
    {
        
    }

    protected bool CanAttack()
    {
        return targetAttack != null;
    }

    protected void DoAttack()
    {
        characterAnimator.SetAttack(CharacterAnimator.AttackType.nomal);
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
    }
}
