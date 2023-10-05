using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public enum AnimationState { }
    public enum AttackType { nomal, power }

    private Animator animator = null;

    protected AttackType currentAttackType;

    protected string currentTrigger = "";
    public Animator Ator
    {
        get
        {
            if (animator == null)
                GetComponent<Animator>();
            return animator;
        }
    }
    public void Initialized()
    {
        
    }
    public void SetAttack(AttackType type)
    {

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
    public void SetBool(string param, bool value)
    {
        Ator.SetBool(param, value);
    }
    public void SetFloat(string param, float value)
    {
        Ator.SetFloat(param, value);
    }
}
