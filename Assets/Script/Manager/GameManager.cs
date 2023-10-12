using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Manager")]
    public AssetManager assetManager;
    [Header("Camera")]
    public Transform cameraTarget = null;
    [Header("Enemy")]
    public List<Enemy> enemies = null;
    public List<WayPoint> enemyWayPoints = null;
    
    
    [Header("Player Infomation")]
    public Player player = null;
    public Transform hand;
    
    //public Func<float> getEnemyDamage = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        //assetManager.InstantiateSword(assetManager.Weapon, hand);
        // getEnemyDamage
    }
    private void Start()
    {
        
    }
    public void Add(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void Remove(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
    private void OnDestroy()
    {
        Instance = null;
    }



}
