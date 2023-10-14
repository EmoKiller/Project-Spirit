using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChildrenSlider : MonoBehaviour
{
    public AttributeType type;
    public Slider sliders;
    public TMP_Text text;

    public void UpdateSlider(float maxValue)
    {
        sliders.maxValue = maxValue;
        sliders.value = maxValue;
        text.text = sliders.maxValue.ToString() + " / " + sliders.maxValue.ToString();
    }
    public void OnReduceValueChanged(float Reduce)
    {
        sliders.value -= Reduce;
        text.text = sliders.value.ToString() + " / " + sliders.maxValue.ToString();
    }
    public void OnIncreaseValueChanged(float Reduce)
    {
        sliders.value += Reduce;
        text.text = sliders.value.ToString() + " / " + sliders.maxValue.ToString();
    }

}
