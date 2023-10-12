using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] List<SliderPlayer> sliderPlayers = new List<SliderPlayer>();
    
    public void UpdateFullSliderPlayer()
    {
        for (int i = 0; i < sliderPlayers.Count; i++)
        {
            sliderPlayers[i].UpdateSlider(10);
        }
    }
    public void UpdateSlider(SliderPlayerType type)
    {
        sliderPlayers[(int)type].UpdateSlider(20);
    }




    private void Start()
    {
        UpdateFullSliderPlayer();
        UpdateSlider(SliderPlayerType.HP);
    }



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    private void OnDestroy()
    {
        Instance = null;
    }
}
