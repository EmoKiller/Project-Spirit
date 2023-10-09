    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Player : CharacterBrain
{

    //[SerializeField] protected Joystick joyStick = null;
    
    private float horizontal { get; set; }
    private float vertical { get; set; }

    private int dirMove = 0;
    protected override CharacterBrain targetAttack => throw new System.NotImplementedException();

   

    protected override void Awake()
    {
        
        base.Awake();
    }
    protected void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            dirMove = horizontal > 0 ? 3 : 2;
            
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            dirMove = vertical > 0 ? 1 : 0;
            
        }
        agent.MoveToDirection(new Vector3(horizontal, 0, vertical));
        if (horizontal != 0 || vertical!=0)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Walk);
        }
        else
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        }
        ManagerDirectionMove.SetActiveDirectionMove((DirectionMove)dirMove);
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            
            

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
