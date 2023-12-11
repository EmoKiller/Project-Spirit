using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    int level = 1;
    int round = 1;
    private void Start()
    {
    }
    private void SpawnEnemy()
    {
        foreach (var item in ConfigDataHelper.GameConfig.GameLevelConfig[level].Round[round].ConfigEnemyRound)
        {
            Debug.Log(item.Key + " " + item.Value.value);
            //Instantiate();
            
        }
    }
}
