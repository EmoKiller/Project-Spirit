
using System.Collections.Generic;

public class GameConfig
{
    public Dictionary<ChestType, ChestConfig> ChestConfig = null;
    public Dictionary<int, LevelConfig> GameLevelConfig = null;
    public Dictionary<CardType, CardConfig> cardsConfig = null;
    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> SaveSlot = null;
    public Dictionary<SaveGameSlot, Dictionary<ShopPowerAttributes, BaseShopPowerAddattributes>> SavePowerAddattributes = null;




    public Dictionary<AttributeType, BaseAttribute> HeroBaseData = null;
    //public Dictionary<AttributeType, BaseAttribute> BaseAttributes = null;
    public Dictionary<AttributeType, BaseAttribute> TarrotAddattributes = null;
    public Dictionary<AttributeType, BaseAttribute> PowerAddattributes = null;
    public Dictionary<ShopPowerAttributes, BaseShopPowerAddattributes> ShopPowerAddattributes = null;
    public Dictionary<ShopPowerAttributes, BaseShopPowerAddattributes> ValuePowerUpbought = null;
    public Dictionary<TypeLevelDifficult,Dictionary<TypeControlDifficult, LevelDifficult>> GameDifficult = null;

    public GameConfig()
    {

    }
}
