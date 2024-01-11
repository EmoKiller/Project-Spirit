using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;
    int level = 1;
    int round = 1;
    [SerializeField] public List<Enemy> listEnemys = new List<Enemy>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public void Init()
    {
        this.DelayCall(10f, () =>
        {
            SpawnEnemy();
        });
        
    }
    public void SpawnEnemy()
    {
        foreach (var item in DataGameLevelConfig())
        {
            for (int i = 0; i < item.Value.value; i++)
            {
                Enemy ene = ObjectPooling.Instance.PopEnemy(item.Value.type, false);
                ene.transform.position = (UnityEngine.Random.onUnitSphere * 80) + ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position;
                ene.transform.SetParent(transform, true);
                ene.SetLevelEnemy(item.Value.LevelEnemy);
                ene.Init();
                ene.Show();
                listEnemys.Add(ene);
            }
        }
    }
    public void RemoveInList(Enemy ene)
    {
        listEnemys.Remove(ene);
        if (listEnemys.Count == 0)
        {
            Debug.Log("ClearEnemy");
            RewardSystem.Instance.SpawnChestBonus(DataGameLevelReward(),transform.position);
            NextRound();
            this.DelayCall(20, () =>
            {
                SpawnEnemy();
            });
        }
    }
    private void NextRound()
    {
        round++;
    }
    private Dictionary<TypeEnemy,ConfigEnemy> DataGameLevelConfig()
    {
        return ConfigDataHelper.GameConfig.GameLevelConfig[level].rounds[round].enemies;
    }
    private ChestType DataGameLevelReward()
    {
        return ConfigDataHelper.GameConfig.GameLevelConfig[level].rounds[round].RewardChest;
    }






}
