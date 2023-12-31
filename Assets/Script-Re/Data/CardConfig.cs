using System;

public class CardConfig
{
    public string type;
    public string attributeAdded;
    public float valueAdded;
    public string quote;
    public string description;

    public CardType Type => (CardType)Enum.Parse(typeof(CardType), type);
    public AttributeType AttributeAdded => (AttributeType)Enum.Parse(typeof(AttributeType), attributeAdded);
    public CardConfig()
    {

    }
}
