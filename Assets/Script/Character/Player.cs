    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Player : CharacterBrain
{

    //[SerializeField] protected Joystick joyStick = null;
    [Header("weapon")]
    public GameObject weapon;
    public Transform hand;
    public float horizontal { get; set; }
    public float vertical { get; set; }
    protected override CharacterBrain targetAttack => throw new System.NotImplementedException();

    

    protected override void Awake()
    {
        
        base.Awake();
    }
    protected void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        agent.MoveToDirection(new Vector3(horizontal, 0, vertical));
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Instantiate(weapon, hand);
            InstantiateSword();
        }
    }
    private void InstantiateSword()
    {
        //GameObject weaponObj = Resources.Load<GameObject>(string.Format(GameConstants.Sword, "AdvancedShortSword"));
        //Instantiate();
        GameObject weapon = null;
        //Addressables.InstantiateAsync("Assets/Prefabs/Weapon/Sword/AdvancedShortSword.prefab").Completed += (handle) => 
        //{
        //    handle.Result
        //    //Instantiate(weapon, hand);
        //};

        Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Weapon/Sword/AdvancedShortSword.prefab").Completed += (handle) =>
        {
            weapon = handle.Result;
            Instantiate(weapon, hand);
        };
    }

}
