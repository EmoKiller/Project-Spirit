using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected Slider hpSliderPlayer;
    [SerializeField] protected Slider mpSliderPlayer;
    [SerializeField] protected Slider spSliderPlayer;
    public void UpdateSlider(float Health, float mana, float Stanima, float currentHealth, float currentMana, float currentStamina)
    {
        hpSliderPlayer.maxValue = Health;
        mpSliderPlayer.maxValue = mana;
        spSliderPlayer.maxValue = Stanima;
        hpSliderPlayer.value = currentHealth;
        mpSliderPlayer.value = currentMana;
        spSliderPlayer.value = currentStamina;

    }
}
