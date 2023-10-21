using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public enum AnimationState { Movement, Attack }
    public enum MovementType { Idle, Walk, Run }
    public enum AttackType { nomal, power }

    private Animator ator = null;
    [SerializeField] protected AnimationState currentAnimationState;
    [SerializeField] protected MovementType currentMovementType;
    [SerializeField] protected AttackType currentAttackType;

    [SerializeField] protected string currentTrigger = "";
    public Animator Ator
    {
        get
        {
            if (ator == null)
                GetComponent<Animator>();
            return ator;
        }                                                          
    }
    public void Initialized()
    {
        ator = GetComponent<Animator>();
    }
    public void SetMovement(MovementType type)
    {
        if (currentAnimationState == AnimationState.Movement && currentMovementType == type)
            return;

        //SetFloat("MovementType", (int)type);
        //SetTrigger("Movement");
        SetInt("State", (int)type);

        currentAnimationState = AnimationState.Movement;
        currentMovementType = type;
    }

    public void SetAttack(AttackType type)
    {
        if (currentAnimationState == AnimationState.Attack && currentAttackType == type)
            return;

        SetFloat("AttackType", (int)type);
        SetTrigger("Attack");

        currentAnimationState = AnimationState.Attack;
        currentAttackType = type;
    }
    public void SetTrigger(string param)
    {
        if (param.Equals(currentTrigger))
            return;
        if (!String.IsNullOrEmpty(currentTrigger))
            Ator.ResetTrigger(currentTrigger);

        Ator.SetTrigger(param);
        currentTrigger = param;
    }
    public void ResetTrigger()
    {
        currentTrigger = "";
    }
    public void SetBool(string param, bool value)
    {
        Ator.SetBool(param, value);
    }
    public void SetFloat(string param, float value)
    {
        Ator.SetFloat(param, value);
    }
    public void SetInt(string param, int value)
    {
        Ator.SetInteger(param, value);
    }



    
}
