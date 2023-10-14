using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] List<ChildrenSlider> sliderPlayers = new List<ChildrenSlider>();
    
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
