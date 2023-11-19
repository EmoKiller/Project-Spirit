using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dungeon Infomation")]
    public string NameDungeon = "";
    public string NamePhase = "";
    [Header("Enemy")]
    public List<Enemy> enemies = null;
    public List<WayPoint> enemyWayPoints = null;




    private void Awake()
    {

    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
    }

    private void ChestBonus()
    {

    }


}

