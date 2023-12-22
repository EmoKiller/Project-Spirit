using UnityEngine;
using UnityEngine.UI;

public class UIFillAttribute : UI_Attribute
{
    [SerializeField] Image fill = null;
    
    protected override void OnValueChanged(AttributeType type, float newValue)
    {
        if (type != this.type)
            return;
        fill.fillAmount = newValue / InfomationPlayerManager.Instance.GetValueAttribute(typeMaxValue);

    }
}
