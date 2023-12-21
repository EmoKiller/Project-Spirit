using System;
using System.Collections.Generic;

public class HeroData : ICloneable
{
    public float Level = 0;

    public Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>> attributes = new Dictionary<SaveGameSlot, Dictionary<AttributeType, BaseAttribute>>();
    public HeroData() { }
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
