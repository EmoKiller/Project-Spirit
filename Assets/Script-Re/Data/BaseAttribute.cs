using System;

public class BaseAttribute
{
    public string type;
    public float value;
    public AttributeType Type => (AttributeType)Enum.Parse(typeof(AttributeType), type);
    public Action<float> OnValueChange = null;
    public BaseAttribute()
    {

    }
    public BaseAttribute(string type, float value)
    {
        this.type = type;
        this.value = value;
    }
    public BaseAttribute(BaseAttribute obj)
    {
        this.type = obj.type;
        this.value = obj.value;
    }

}
