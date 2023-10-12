using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderPlayer : MonoBehaviour 
{
    public abstract void UpdateSlider(float maxValue);
}

public class ChildrenSlider : SliderPlayer
{
    public SliderPlayerType type;
    public Slider sliders;
    public TMP_Text text;

    public override void UpdateSlider(float maxValue)
    {
        sliders.maxValue = maxValue;
        sliders.value = maxValue;
        text.text = maxValue.ToString() + " / " + maxValue.ToString();
    }
}
