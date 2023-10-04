using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] protected string characterName = "";
    [SerializeField] protected float hp = 5f;

    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    //[SerializeField] protected CharacterAnimator characterAnimator = null;
    //[SerializeField] protected CharacterAttack characterAttack = null;

    public string Name => characterName;
    protected abstract CharacterBrain targetAttack { get; }



    protected virtual void Awake()
    {
        agent.Initialized();
        characterName = gameObject.name;
    }


    protected bool CanAttack()
    {
        return targetAttack != null;
    }

    protected void DoAttackNomal()
    {
        
    }
    protected void DoAttackHeavy()
    {

    }
}
