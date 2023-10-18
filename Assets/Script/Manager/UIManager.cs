using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(this);
    }
    private void OnDisable()
    {
        Instance = null;
    }

    //public void OnNotify()
    //{
    //    Debug.Log("UIManager OnNotify");
    //    UpdateFullSliderPlayer();
    //    UpdateSlider(AttributeType.HP);
    //}
    //private void OnEnable()
    //{
    //    EventDispatcher.TriggerEvent(Events.OnHealthChanged);
    //}
    //private void OnDisable()
    //{
    //    EventDispatcher.RemoveListener(Events.OnHealthChanged, OnPlayerHealthChanged);
    //}

    //private void OnPlayerHealthChanged()
    //{
    //    Debug.Log("GameManager Trigger OnHealthChange");
    //}

}
