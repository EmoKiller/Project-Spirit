using System.Collections.Generic;
using UnityEngine;

public class LevelConfig 
{
    public int levelIndex;
    public string levelName;
    public Dictionary<int, RoundConfig> rounds = null;

    public LevelConfig()
    {

    }
}
