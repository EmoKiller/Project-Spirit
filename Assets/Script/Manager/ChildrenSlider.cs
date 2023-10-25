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

    public void UpdateHealth(float health)
    {
        sliders.value = health;
    }


    //public void OnReduceValueChanged(float Reduce)
    //{
    //    sliders.value -= Reduce;
    //}
    //public void OnIncreaseValueChanged(float Reduce)
    //{
    //    sliders.value += Reduce;
    //}
    

}
