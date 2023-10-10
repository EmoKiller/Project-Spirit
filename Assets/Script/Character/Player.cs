using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Player : CharacterBrain
{
    protected override CharacterBrain targetAttack => throw new System.NotImplementedException();
    protected float horizontal => Input.GetAxis("Horizontal");
    protected float vertical => Input.GetAxis("Vertical");

    protected override Vector3 direction => new Vector3(horizontal, 0, vertical);
    [SerializeField] protected JoyStickLManager joyStick = null;
    

    //private float mana = 10f;
    //private float stamina = 10f;
    //private float currentMana = 10f;
    //private float currentStamina = 10f;
    protected override void Awake()
    {
        
        base.Awake();
        
    }
    private void Start()
    {
        
    }
    protected void Update()
    {
        agent.MoveToDirection(direction);
        if (Vector3.Distance(transform.position, transform.position + direction) > 0.3)
            charactorDirectionMove.DirectionMove(transform.position, transform.position + direction, dirNum);
        
        //Debug.Log(joyStick.handle.position.normalized);
        //Debug.Log(Vector2.Distance(joyStick.handle.position.normalized, joyStick.handle.position));
        //if (Vector3.Distance(joyStick.joyStickL.position, joyStick.joyStickL.position + joyStick.handle.position) > 2)
        //    charactorDirectionMove.DirectionMove(joyStick.joyStickL.position, joyStick.joyStickL.position + joyStick.handle.position, dirNum);

        if (horizontal != 0 || vertical!=0)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Walk);
        }
        else
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            //characterAnimator.SetTrigger("Slash1H");


        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            characterAttack.Initialized();
            Debug.Log(characterAttack.Damage);
        }
    }
    
    
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.name == "DropItem")
    //    {
    //        DropItemsOnWorld dropitem = other.GetComponent<DropItemsOnWorld>();
    //        dropitem.showButton.gameObject.SetActive(true);
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            Debug.Log("Pick Up Items");
    //        }
    //    }
    //}
    
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.name == "DropItem")
    //    {
    //        DropItemsOnWorld dropitem = other.GetComponent<DropItemsOnWorld>();
    //        dropitem.showButton.gameObject.SetActive(false);
    //    }
    //}

}
