using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent<string,CharacterBrain> OnCharacterBrainEvent = new UnityEvent<string,CharacterBrain>();

    [Header("Camera")]
    public CameraFollow Camera;
    [Header("Enemy")]
    public List<Enemy> enemies = null;
    public List<WayPoint> enemyWayPoints = null;
    [Header("EnemyAction")]
    public Action<Enemy> addTargetForEnemy = null;
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
    private void Awake()
    {
        addTargetForEnemy = AddTargetAttackFor;
        ChangeTargetOfCamera(player.PointTargetOfCamera);
        addTargetForEnemy?.Invoke(enemies[0]);
    }
    private void ChangeTargetOfCamera(Transform target)
    {
        Camera.target = target;
    }
    private void AddTargetAttackFor(Enemy ene)
    {
        ene.targetAttack = player;
    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnHealthChanged, OnPlayerHealthChanged);
        //EventDispatcher.AddListener(Events.OnAttack, OnHit);
        
    }

    private void OnDisable()
    {
        OnCharacterBrainEvent = null;
        EventDispatcher.RemoveListener(Events.OnHealthChanged, OnPlayerHealthChanged);
        
    }
    private void OnPlayerHealthChanged()
    {
        Debug.Log("GameManager Trigger OnHealthChange");
    }



}

