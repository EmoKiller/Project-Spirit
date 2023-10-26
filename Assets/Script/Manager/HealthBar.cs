using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _maxHealth;
    private float _health;

    [SerializeField] private Slider slider;

    private void Start()
    {
        if (_maxHealth == 0)
            _maxHealth = 1;
    }

    public void SetHealh(float maxHealth)
    {
        _maxHealth = maxHealth;
        _health = maxHealth;
    }

    public void UpdateHealth(float health)
    {
        this._health = health;
        slider.value = health/_maxHealth;
    }
}
