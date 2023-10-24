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
    [SerializeField] public CharacterAttack characterAttack = null;
    
    
    protected virtual Vector3 direction { get; }
    protected abstract bool Alive { get; }
    protected int dirNum = 0;
    public string Name => characterName;
    public MeshAgent Agent => agent;

    protected virtual void Awake()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterName = gameObject.name;
    }
    public void DoAttack()
    {
        characterAnimator.SetAttack(CharacterAnimator.AttackType.nomal);
    }
    public virtual void OnHit() { }


}
