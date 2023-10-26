using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;

    //BaseCharacter
    protected string characterName {get; set;}
    protected float health { get; set; }
    protected float maxHealth { get; set; }
    public bool Alive => health >= 0;
    public virtual string Name => characterName;
    
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

    public abstract void TakeDamage(float damage);

}
