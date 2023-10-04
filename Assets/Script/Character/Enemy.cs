using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBrain
{
    [Header("Attack")]
    [SerializeField] protected float attackRange = 5f;
    protected override CharacterBrain targetAttack => GameManager.Instance.player;

    

    protected bool arried = false;
    protected bool onFollowPlayer = false;

    protected override void Awake()
    {
        agent = GetComponent<MeshAgent>();
        base.Awake();
        agent.OnArried = OnArried;
        
    }
    void Update()
    {

    }
    protected virtual void OnArried()
    {
        arried = true;
    }
}
