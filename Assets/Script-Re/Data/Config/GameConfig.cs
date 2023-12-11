
using System.Collections.Generic;

public class GameConfig
{
    public Dictionary<ChestType, ChestConfig> ChestConfig = null;
    public Dictionary<int,GameLevelConfig> GameLevelConfig = null;
    public Dictionary<TypeSave, SaveGameIndex> SaveGames = new Dictionary <TypeSave,SaveGameIndex>();
    public GameConfig()
    {

    }
}
