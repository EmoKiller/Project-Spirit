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
    [Header("Player Infomation")]
    public Player player = null;
    public Transform hand;
    [Header("Camera")]
    public AssetManager assetManager;
    [Header("Camera")]
    public CameraFollow Camera;

    private void Awake()
    {
        enemies[0].SetTarget(player.transform);
    }
    private void ChangeTargetOfCamera(Transform target)
    {
        Camera.target = target;
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        OnCharacterBrainEvent = null;
    }



}

