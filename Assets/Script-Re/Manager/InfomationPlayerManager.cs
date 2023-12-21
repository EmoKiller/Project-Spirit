using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InfomationPlayerManager : SerializedMonoBehaviour
{
    public enum Script
    {
        InfomationPlayerManager
    }
    public static InfomationPlayerManager Instance = null;
    public SaveGameSlot SaveSlot;
    [SerializeField] private HeroData heroData = null;
    [SerializeField] private AnimationCurve MaxExpPerLevelCurve;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        heroData = (HeroData)ConfigDataHelper.HeroData.Clone();
    }
    public void Init()
    {
        //AttributeOnChange(AttributeType.Level,1);
        //AttributeOnChange(AttributeType.MaxRedHeart, 0);
        //SaveGame();
    }
    public float GetValueAttribute(AttributeType type)
    {
        return heroData.attributes[SaveSlot][type].value;
    }
    [Button]
    public void IncreaseValueOf(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].value += value;
        ObseverConstants.OnAttributeValueChanged?.Invoke(type, GetValueAttribute(type));
    }
    [Button]
    public void MinusValueOf(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].value -= value;
        ObseverConstants.OnAttributeValueChanged?.Invoke(type, GetValueAttribute(type));
    }
    [Button]
    public void UpdateValueOf(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].value = value;
        ObseverConstants.OnAttributeValueChanged?.Invoke(type, value);
    }
    public bool CompareCurrentNMaxAttributes(AttributeType Current, AttributeType Max)
    {
        return heroData.attributes[SaveSlot][Current].value == heroData.attributes[SaveSlot][Max].value;
    }


    public void StartGame()
    {
        
    }
    private void SaveGame()
    {
        ConfigDataHelper.HeroData = heroData;
    }
}
