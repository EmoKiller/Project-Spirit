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

    public string Name => characterName;
    protected abstract CharacterBrain targetAttack { get; }



    protected virtual void Awake()
    {
        agent = GetComponent<MeshAgent>();
        agent.Initialized();
        characterName = gameObject.name;
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
