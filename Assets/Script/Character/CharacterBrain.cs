using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected string characterName = "";
    [SerializeField] protected float Health = 5f;
    [SerializeField] protected float CurrentHealth = 5f;
    [SerializeField] protected bool Alive => CurrentHealth > 0;

    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected ManagerDirectionMove ManagerDirectionMove = null;
    public string Name => characterName;
    protected abstract CharacterBrain targetAttack { get; }

    public MeshAgent Agent => agent;
    public CharacterAttack CharacterAtk => characterAttack;

    protected virtual void Awake()
    {
        ManagerDirectionMove = GetComponentInChildren<ManagerDirectionMove>();
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        agent.Initialized();
        characterAnimator.Initialized();
        ManagerDirectionMove.Initialized();
        characterName = gameObject.name;
    }
    private void Start()
    {
        ManagerDirectionMove.SetActiveDirectionMove(DirectionMove.Front);
    }

    protected bool CanAttack()
    {
        return targetAttack != null;
    }

    protected void DoAttackNomal()
    {
        //characterAnimator.SetTrigger();
    }
    protected void DoAttackHeavy()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
    }
}
