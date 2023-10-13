using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public static UIManager Instance;
    [SerializeField] List<SliderPlayer> sliderPlayers = new List<SliderPlayer>();
    
    public void UpdateFullSliderPlayer()
    {
        for (int i = 0; i < sliderPlayers.Count; i++)
        {
            sliderPlayers[i].UpdateSlider(10);
        }
    }
    public void UpdateSlider(AttributeType type)
    {
        sliderPlayers[(int)type].UpdateSlider(20);
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
