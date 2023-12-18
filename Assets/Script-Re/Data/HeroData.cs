using System.Collections.Generic;

public class HeroData
{
    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> attributes = new Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>>();
}
