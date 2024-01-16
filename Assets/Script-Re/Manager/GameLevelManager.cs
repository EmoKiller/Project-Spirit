using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameLevelManager : MonoBehaviour
{
    public static GameLevelManager Instance;

    [SerializeField] int level = 1;
    [SerializeField] int round = 1;
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
        //SpawnObj(GameConstants.SkillCurses, ((NameCurses)UnityEngine.Random.Range(0, InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.TypeLvlCruses) + 1)).ToString(), cursesPodium);
        SpawnObj(GameConstants.SkillCurses, NameCurses.DeatsSweep.ToString(), cursesPodium);
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
                Vector3 random = (UnityEngine.Random.onUnitSphere * 80);
                Vector3 position = new Vector3(random.x, 0, random.z);
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
            RewardSystem.Instance.SpawnChestBonus(DataGameLevelReward(), transform.position, out ChestBonus chest);
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
