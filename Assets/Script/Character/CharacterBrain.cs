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
    protected abstract bool Alive { get; }
    protected int dirNum = 0;
    public string Name => characterName;

    
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
    protected bool CanAttack()
    {
        return targetAttack != null;
    }

    public void DoAttack()
    {
        characterAnimator.SetAttack(CharacterAnimator.AttackType.nomal);
    }
    
}
