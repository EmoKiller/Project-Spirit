    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBrain
{
    
    //[SerializeField] protected Joystick joyStick = null;

    public float horizontal { get; set; }
    public float vertical { get; set; }
    protected override CharacterBrain targetAttack => throw new System.NotImplementedException();

    

    protected override void Awake()
    {
        agent = GetComponent<MeshAgent>();
        base.Awake();
    }
    protected void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        agent.MoveToDirection(new Vector3(horizontal, 0, vertical));
        if (Input.GetKeyDown(KeyCode.J))
        {
            
        }
    }
}
