using UnityEngine;
using UnityEngine.UI;

public class ShowHPEnemy : MonoBehaviour
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
}
