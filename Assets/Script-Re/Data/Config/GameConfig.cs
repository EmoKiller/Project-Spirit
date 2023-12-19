
using System.Collections.Generic;

public class GameConfig
{
    public Dictionary<ChestType, ChestConfig> ChestConfig = null;
    public Dictionary<int, LevelConfig> GameLevelConfig = null;
    public Dictionary<CardType, CardConfig> cardsConfig = null;
    public Dictionary<AttributeType, BaseAttribute> HeroBaseData = null;

    public GameConfig()
    {

    }
}
