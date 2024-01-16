using System;

public class BaseShopPowerAddattributes 
{
    public string name;
    public string type;
    public string attributeAdded;
    public float valueAdded;
    public string quote;
    public float price;
    public float numberTick;
    public AttributeType Type => (AttributeType)Enum.Parse(typeof(AttributeType), type);
    public AttributeType AttributeAdded => (AttributeType)Enum.Parse(typeof(AttributeType), attributeAdded);
    public BaseShopPowerAddattributes()
    {

    }
}
