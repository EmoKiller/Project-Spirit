using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;

    int level = 1;
    int round = 1;
    [SerializeField] public List<Enemy> listEnemys = new List<Enemy>();
    [SerializeField] private List<Enemy> EnemySummon = new List<Enemy>();
    [SerializeField] Transform weaponPodium;
    [SerializeField] Transform cursesPodium;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        ObseverConstants.OnClickButtonStart.AddListener(Init);
        ObseverConstants.OnClickButtonContinue.AddListener(ResetGameLevel);
    }
    public void Init()
    {
        SpawnObj(GameConstants.WeaponSword, ((TypeSword)UnityEngine.Random.Range(0, InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.TypeLvlSword) + 1)).ToString(), weaponPodium);

        this.DelayCall(10f, () =>
        {
            SpawnEnemy();
        });

    }
    public void SpawnObj(string path, string objectName, Transform transform)
    {
        GameObject objAsset = Addressables.LoadAssetAsync<GameObject>(string.Format(path, objectName)).WaitForCompletion();
        Instantiate(objAsset, transform);
    }
    public void ResetGameLevel()
    {
        level = 1;
        round = 1;
        for (int i = listEnemys.Count - 1; i >= 0; i--)
        {
            listEnemys[i].Hide();
            listEnemys.Remove(listEnemys[i]);
        }
    }
    public void SpawnEnemy()
    {
        foreach (var item in DataGameLevelConfig())
        {
            for (int i = 0; i < item.Value.value; i++)
            {
                Vector3 position = (UnityEngine.Random.onUnitSphere * 80) + ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position;
                PopEnemyFromPool(listEnemys, item.Value.type, item.Value.LevelEnemy, position);
            }
        }
    }
    private void PopEnemyFromPool(List<Enemy> listEnemy, string name, LevelRomanNumerals levelEnemy, Vector3 pos)
    {
        Enemy ene = ObjectPooling.Instance.PopEnemy(name, false);
        ene.transform.position = pos;
        ene.transform.SetParent(transform, true);
        ene.SetLevelEnemy(levelEnemy);
        ene.Init();
        ene.Show();
        listEnemy.Add(ene);
    }
    public void SummonEnemy(Vector3 pos)
    {
        PopEnemyFromPool(EnemySummon, TypeEnemy.EneScamp.ToString(), LevelRomanNumerals.I, pos);
    }
    public void RemoveSummonEnemy(Enemy ene)
    {
        EnemySummon.Remove(ene);
    }
    public void CheckAllEnemyDead(Enemy ene)
    {
        if (!listEnemys.Contains(ene))
            return;
        listEnemys.Remove(ene);
        if (listEnemys.Count == 0)
        {
            Debug.Log("ClearEnemy");
            RewardSystem.Instance.SpawnChestBonus(DataGameLevelReward(), transform.position);
            NextRound();
            this.DelayCall(5, () =>
            {
                SpawnEnemy();
            });
        }
    }
    private void NextRound()
    {
        round++;
    }
    private Dictionary<TypeEnemy, ConfigEnemy> DataGameLevelConfig()
    {
        return ConfigDataHelper.GameConfig.GameLevelConfig[level].rounds[round].enemies;
    }
    private ChestType DataGameLevelReward()
    {
        return ConfigDataHelper.GameConfig.GameLevelConfig[level].rounds[round].RewardChest;
    }






}
