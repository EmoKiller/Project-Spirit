using UnityEngine;
using UnityEngine.UI;


public class ChildrenSlider : MonoBehaviour
{
    public Slider sliders;
    public void UpdateSlider(float maxValue)
    {
        sliders.maxValue = maxValue;
        sliders.value = maxValue;
        return;
    }
    public void OnReduceValueChanged(float Reduce)
    {
        sliders.value -= Reduce;
        return;
    }
    public void OnIncreaseValueChanged(float Reduce)
    {
        sliders.value += Reduce;
        return;
    }
    

}
