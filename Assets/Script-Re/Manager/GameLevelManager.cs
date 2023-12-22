using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    int level = 1;
    int round = 1;
    private void Start()
    {
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        foreach (var item in ConfigDataHelper.GameConfig.GameLevelConfig[level].rounds[round].enemies)
        {
            Debug.Log(item);
            
        }
    }

}
