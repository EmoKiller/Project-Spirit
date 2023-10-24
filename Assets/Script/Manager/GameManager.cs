using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent<string,CharacterBrain> OnCharacterBrainEvent = new UnityEvent<string,CharacterBrain>();
    [Header("Dungeon Infomation")]
    public string NameDungeon = "";
    public string NamePhase = "";
    [Header("Enemy")]
    public List<Enemy> enemies = null;
    public List<WayPoint> enemyWayPoints = null;
    [Header("EnemyAction")]
    public Action<Enemy> addTargetForEnemy = null;
    [Header("Player Infomation")]
    public Player player = null;
    public Transform hand;
    [Header("Camera")]
    public AssetManager assetManager;
    [Header("Camera")]
    public CameraFollow Camera;

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
        EventDispatcher.AddListener(Events.OnEnemyHit, OnEnemyHit);
    }
    private void OnDisable()
    {
        OnCharacterBrainEvent = null;
        EventDispatcher.RemoveListener(Events.OnHealthChanged, OnPlayerHealthChanged);
        EventDispatcher.RemoveListener(Events.OnEnemyHit, OnEnemyHit);

    }
    private void OnEnemyHit()
    {
        assetManager.InstantiateItems(assetManager.SlashHit, enemies[0].transform);
    }
    private void OnPlayerHealthChanged()
    {
        Debug.Log("GameManager Trigger OnHealthChange");
    }



}

