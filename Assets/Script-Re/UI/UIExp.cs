using UnityEngine;
using UnityEngine.UI;

public class UIExp : MonoBehaviour
{
    private Slider _slider => GetComponent<Slider>();
    public float MaxValue
    {
        get { return _slider.maxValue; }
        set { OnMaxValueChange(value); }
    }
    public float Value
    {
        get { return _slider.value; }
        set { _slider.value += value; }
    }
    private void OnMaxValueChange(float value)
    {
        _slider.maxValue = value;
        Value = 0;
    }
}
