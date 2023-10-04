using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    //enemy
    public List<WayPoint> enemyWayPoints = null;
    public List<Enemy> enemies = null;
    //

    public Player player = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
