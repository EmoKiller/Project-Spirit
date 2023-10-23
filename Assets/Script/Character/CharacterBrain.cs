using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected string characterName = "";
    
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    
    //protected virtual CharacterBrain targetAttack { get; }
    protected virtual Vector3 direction { get; }
    protected abstract bool Alive { get; }
    protected int dirNum = 0;
    public string Name => characterName;

    
    public MeshAgent Agent => agent;
    public CharacterAttack CharacterAtk => characterAttack;

    protected virtual void Awake()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterName = gameObject.name;
    }
    //protected bool CanAttack()
    //{
    //    return targetAttack != null;
    //}

    public void DoAttack()
    {
        characterAnimator.SetAttack(CharacterAnimator.AttackType.nomal);
    }
    public virtual void OnHit() { }


}
