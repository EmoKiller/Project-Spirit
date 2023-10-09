using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    
    [Header("Manager")]
    public AssetManager assetManager;
    [Header("Camera")]
    public Transform cameraTarget = null;
    [Header("Enemy")]
    public List<WayPoint> enemyWayPoints = null;
    public List<Enemy> enemies = null;
    
    [Header("Player Infomation")]
    public Player player = null;
    public Transform hand;

    private void Start()
    {
        

    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        //assetManager.InstantiateSword(assetManager.Weapon, hand);
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
