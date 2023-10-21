using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance = null;
    public Camera Camera;

    [Header("Camera")]
    public Transform cameraTarget = null;
    [Header("Enemy")]
    public List<Enemy> enemies = null;
    public List<WayPoint> enemyWayPoints = null;
    
    
    [Header("Player Infomation")]
    public Player player = null;
    public Transform hand;
    
    //public Func<float> getEnemyDamage = null;
    //private void Awake()
    //{
    //    if (Instance == null)
    //        Instance = this;
    //    else
    //        Destroy(Instance);
    //    //assetManager.InstantiateSword(assetManager.Weapon, hand);
    //    // getEnemyDamage
    //}
    //private void OnDestroy()
    //{
    //    Instance = null;
    //}

    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnHealthChanged, OnPlayerHealthChanged);
        //EventDispatcher.AddListener(Events.OnAttack, OnHit);
    }

    private void OnDisable()
    {
        EventDispatcher.RemoveListener(Events.OnHealthChanged, OnPlayerHealthChanged);
        //EventDispatcher.RemoveListener(Events.OnAttack, OnHit);
    }
    //private void OnEnable()
    //{
    //    EventDispatcher.AddListener(Events.OnAttack, OnHit);
    //}
    //private void OnDisable()
    //{
    //    EventDispatcher.RemoveListener(Events.OnAttack, OnHit);
    //}

    private void OnPlayerHealthChanged()
    {
        Debug.Log("GameManager Trigger OnHealthChange");
    }



}

