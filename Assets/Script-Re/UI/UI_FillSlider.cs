using UnityEngine;
using UnityEngine.UI;

public class UI_FillSlider : UI_Attribute
{
    [SerializeField] Slider slider = null;
    protected override void OnValueChanged(AttributeType type, float newValue)
    {
        if (type != this.type)
            return;
        slider.value = newValue;
        slider.maxValue = InfomationPlayerManager.Instance.GetValueAttribute(typeMaxValue);
        valueText.text = slider.value.ToString() + " / " + slider.maxValue.ToString();

    }
}
