using System;
using System.Collections.Generic;

public class HeroData : ICloneable
{
    public Dictionary<SaveGameSlot, OnScenes> PlayerOnSceness = new Dictionary<SaveGameSlot, OnScenes>();
    public Dictionary<SaveGameSlot, TypeLevelDifficult> GameDifficult = new Dictionary<SaveGameSlot, TypeLevelDifficult>();
    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> attributes = new Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>>();
    public Dictionary<SaveGameSlot, bool> IsSelectedDifficult = new Dictionary<SaveGameSlot, bool>();
    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> BaseAttributes = new Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>>();
    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> TarrotAddattributes = new Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>>();
    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> PowerAddattributes = new Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>>();

    public HeroData() { }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
