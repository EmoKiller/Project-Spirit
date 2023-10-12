using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Player : CharacterBrain
{
    protected override CharacterBrain targetAttack => GameManager.Instance.enemies.Find(
        e => Vector3.Distance(transform.position, e.gameObject.transform.position) <= characterAttack.AttackRange); 
    protected float horizontal => Input.GetAxis("Horizontal");
    protected float vertical => Input.GetAxis("Vertical");

    protected override Vector3 direction => new Vector3(horizontal, 0, vertical);
    [SerializeField] protected JoyStickLManager joyStick = null;
    
    protected override void Awake()
    {
        
        base.Awake();
        
    }
    private void Start()
    {
        agent.moveSpeed = 4f;
    }
    protected void Update()
    {
        agent.MoveToDirection(direction);
        charactorDirectionMove.DirectionMove(transform.position, transform.position + direction, dirNum);
        
        //Debug.Log(joyStick.handle.position.normalized);
        //Debug.Log(Vector2.Distance(joyStick.handle.position.normalized, joyStick.handle.position));
        //if (Vector3.Distance(joyStick.joyStickL.position, joyStick.joyStickL.position + joyStick.handle.position) > 2)
        //    charactorDirectionMove.DirectionMove(joyStick.joyStickL.position, joyStick.joyStickL.position + joyStick.handle.position, dirNum);

        if (horizontal != 0 || vertical!=0)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Run);
        }
        else
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //characterAnimator.SetTrigger("Slash1H");
            Debug.Log(targetAttack.Name);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //characterAttack.Initialized();
            //Debug.Log(characterAttack.Damage);
            characterAnimator.SetTrigger("Slash2H");
            //UIManager.Instance.AddSlider();
        }
    }
    

}
