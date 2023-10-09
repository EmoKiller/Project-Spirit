    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Player : CharacterBrain
{

    //[SerializeField] protected Joystick joyStick = null;
    [SerializeField] protected Slider hpSlider;
    [SerializeField] protected Slider mpSlider;
    [SerializeField] protected Slider spSlider;
    protected float horizontal { get{ return Input.GetAxis("Horizontal"); } set {  } }
    protected float vertical { get { return Input.GetAxis("Vertical"); } set {  } }

    protected override CharacterBrain targetAttack => throw new System.NotImplementedException();
    protected override void Awake()
    {
        
        base.Awake(); 
    }
    protected void Update()
    {
        agent.MoveToDirection(new Vector3(horizontal, 0, vertical));
        if (horizontal != 0 || vertical!=0)
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Walk);
        }
        else
        {
            characterAnimator.SetMovement(CharacterAnimator.MovementType.Idle);
        }
        charactorDirectionMove.DirectionMove(transform.position, new Vector3(horizontal, 0, vertical) * 100, dirMove);
        
        if (Input.GetKeyDown(KeyCode.J))
        {

            characterAnimator.SetTrigger("Slash1H");


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
