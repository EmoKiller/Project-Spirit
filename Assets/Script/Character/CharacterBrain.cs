using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : MonoBehaviour
{
    [Header("Component System")]
    [SerializeField] protected MeshAgent agent = null;
    [SerializeField] protected CharacterAnimator characterAnimator = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected Slash slash = null;
    //BaseCharacter
    protected string CharacterName {get; set;}
    protected float Health { get; set; }
    protected float MaxHealth { get; set; }
    public bool Alive => Health >= 0;
    public virtual string Name => CharacterName;
    
    protected virtual void Awake()
    {
        agent.Initialized();
        characterAnimator.Initialized();
        characterAttack.Initialized();
        CharacterName = gameObject.name;
        slash.SetActiveSlash(false);
    }
    public void DoAttack()
    {
        //characterAnimator.SetAttack(CharacterAnimator.AttackType.nomal);
    }

    public abstract void TakeDamage(float damage);
    public abstract void StartAttack(float damage);
}
