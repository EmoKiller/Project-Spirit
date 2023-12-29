using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;
    int level = 1;
    int round = 1;
    [SerializeField] private List<Enemy> listEnemys = new List<Enemy>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public void Init()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        foreach (var item in ConfigDataHelper.GameConfig.GameLevelConfig[level].rounds[round].enemies)
        {
            Debug.Log(item.Value.type.ToString());
            for (int i = 0; i < item.Value.value; i++)
            {
                Enemy ene = ObjectPooling.Instance.PopEnemy(item.Value.type.ToString());
                ene.transform.position = (UnityEngine.Random.onUnitSphere * 80) + ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position;
                ene.transform.SetParent(transform, true);
                listEnemys.Add(ene);
            }
        }
    }
    public void RemoveinList(Enemy ene)
    {
        listEnemys.Remove(ene);
    }





}
