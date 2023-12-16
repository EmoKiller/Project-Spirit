using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExp : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _TextExp;
    public float MaxValue
    {
        get { return _slider.maxValue; }
        set { OnMaxValueChange(value); }
    }
    public float Value
    {
        get { return _slider.value; }
        set
        { 
            _slider.value = Mathf.Clamp(value, 0, MaxValue);
            TextExp = Value.ToString();
        }
    }
    public string TextExp
    {
        get { return _TextExp.text; }
        set { _TextExp.text = value + " / " + MaxValue.ToString(); }
    }
    public bool MaxExp()
    {
        return Value >= MaxValue;
    }
    private void OnMaxValueChange(float value)
    {
        _slider.maxValue = value;
        Value = 0;
    }
}
