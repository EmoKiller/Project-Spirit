using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider hpSliderPlayer;
    public Slider mpSliderPlayer;
    public Slider spSliderPlayer;

    public TMP_Text hpText;
    public TMP_Text mpText;
    public TMP_Text spText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    public void UpdateValue(Slider slider, float maxValue, float value,TMP_Text text, string textIn)
    {
        slider.maxValue = maxValue;
        slider.value = value;
        text.text = textIn;
    }
}
