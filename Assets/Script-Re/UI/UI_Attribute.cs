using TMPro;
using UnityEngine;

public class UI_Attribute : MonoBehaviour
{
    public AttributeType type;
    public AttributeType typeMaxValue;
    [SerializeField] protected TMP_Text valueText;
    protected virtual void Awake()
    {
        ObseverConstants.OnAttributeValueChanged.AddListener(OnValueChanged);
        OnValueChanged(type, InfomationPlayerManager.Instance.GetValueAttribute(type));
    }
    protected virtual void OnValueChanged(AttributeType type, float newValue)
    {
        if (type != this.type)
            return;
        valueText.text = newValue.ToString();
    }


}
