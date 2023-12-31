using System.Collections.Generic;
using System;
public class RoundConfig 
{
    public Dictionary<TypeEnemy, ConfigEnemy> enemies = null;
    public string chestType;
    public ChestType RewardChest => (ChestType)Enum.Parse(typeof(ChestType), chestType);


    public RoundConfig()
    {

    }
    
}
