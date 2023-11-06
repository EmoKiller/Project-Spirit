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
    Action step1aniAtk = null;
    Action step2aniAtk = null;
    Action step3aniAtk = null;
    Action step4aniAtk = null;
    Action StartAni = null;
    Action FinishAni = null;
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
    public void AddStepAniAtk(Action step1ani, Action step2ani, Action step3ani, Action step4ani)
    {
        this.step1aniAtk = step1ani;
        this.step2aniAtk = step2ani;
        this.step3aniAtk = step3ani;
        this.step4aniAtk = step4ani;
    }
    public void AddStFishAni(Action StartAni, Action FinishAni)
    {
        this.StartAni = StartAni;
        this.FinishAni = FinishAni;
    }
    public void StartAnimation()
    {
        StartAni?.Invoke();
    }
    public void FinishAnimation()
    {
        FinishAni?.Invoke();
    }
    public void Step1AniAtk()
    {
        step1aniAtk?.Invoke();
    }
    public void Step2AniAtk()
    {
        step2aniAtk?.Invoke();
    }
    public void Step3AniAtk()
    {
        step3aniAtk?.Invoke();
    }
    public void Step4AniAtk()
    {
        step4aniAtk?.Invoke();
    }




}
