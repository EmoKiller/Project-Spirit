using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public enum AnimationState { Movement, Attack }
    public enum MovementType { Idle, Run }
    public enum AttackType { nomal, power }
    public enum AttackStep { step1 , step2, step3 }
    
    private Animator ator = null;
    [SerializeField] protected AnimationState currentAnimationState;
    [SerializeField] protected MovementType currentMovementType;
    [SerializeField] protected AttackType currentAttackType;
    //[SerializeField] protected AttackStep currentAttackStep;
    Action step1ani = null;
    Action step2ani = null;
    Action step3ani = null;
    Action step4ani = null;
    public string currentTrigger = "";
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
        SetFloat("Speed", (float)type);

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
    public void SetComboAttack(AttackType type, float step)
    {
        
        if (currentAnimationState == AnimationState.Attack && currentAttackType == type)
            return;

        SetFloat("Attack", step);

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
    public void AddStepAni(Action step1ani, Action step2ani, Action step3ani, Action step4ani)
    {
        this.step1ani = step1ani;
        this.step2ani = step2ani;
        this.step3ani = step3ani;
        this.step4ani = step4ani;
    }
    public void Step1Ani()
    {
        step1ani?.Invoke();
    }
    public void Step2Ani()
    {
        step2ani?.Invoke();
    }
    public void Step3Ani()
    {
        step3ani?.Invoke();
    }
    public void Step4Ani()
    {
        step4ani?.Invoke();
    }




}
