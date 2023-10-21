using UnityEngine;
using UnityEngine.UI;


public class ChildrenSlider : MonoBehaviour
{
    public Slider sliders;

    public void UpdateSlider(float maxValue)
    {
        sliders.maxValue = maxValue;
        sliders.value = maxValue;
    }
    public void OnReduceValueChanged(float Reduce)
    {
        sliders.value -= Reduce;
    }
    public void OnIncreaseValueChanged(float Reduce)
    {
        sliders.value += Reduce;
    }

}
