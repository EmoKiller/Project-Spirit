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
        //heroData.attributes[SaveSlot][AttributeType.Level].OnValueChange = UpdateLevel;
        //heroData.attributes[SaveSlot][AttributeType.MaxRedHeart].OnValueChange = UpdateMaxRedHeart;
        //heroData.attributes[SaveSlot][AttributeType.CurrentRedHeart].OnValueChange = UpdateCurrentRedHeart;
        //heroData.attributes[SaveSlot][AttributeType.MaxRedAddHeart].OnValueChange = UpdateMaxRedAddHeart;
        //heroData.attributes[SaveSlot][AttributeType.CurrentRedAddHeart].OnValueChange = UpdateCurrentRedAddHeart;
        //heroData.attributes[SaveSlot][AttributeType.MaxBlueHeart].OnValueChange = UpdateMaxBlueHeart;
        //heroData.attributes[SaveSlot][AttributeType.CurrentBlueHeart].OnValueChange = UpdateCurrentBlueHeart;
        //heroData.attributes[SaveSlot][AttributeType.MaxBlackHeart].OnValueChange = UpdateMaxBlackHeart;
        //heroData.attributes[SaveSlot][AttributeType.CurrentBlackHeart].OnValueChange = UpdateCurrentBlackHeart;
        //heroData.attributes[SaveSlot][AttributeType.CurrentExp].OnValueChange = UpdateCurrentExp;
        //heroData.attributes[SaveSlot][AttributeType.MaxExpOfLevel].OnValueChange = UpdateMaxEXPOfLevel;
        //heroData.attributes[SaveSlot][AttributeType.MaxValueAngry].OnValueChange = UpdateMaxValueAngry;
        //heroData.attributes[SaveSlot][AttributeType.CurrentAngry].OnValueChange = UpdateCurretAngry;
        //heroData.attributes[SaveSlot][AttributeType.CurrentCoin].OnValueChange = UpdateCurrentCoin;
        //heroData.attributes[SaveSlot][AttributeType.MaxValueHunger].OnValueChange = UpdateMaxValueHunger;
        //heroData.attributes[SaveSlot][AttributeType.CurrentHunger].OnValueChange = UpdateCurrentHunger;
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
    
    /// <summary>
    /// /
    /// </summary>
    private void IncreaseValueOf(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].value += value;
    }
    private void UpdateValueOf(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].value = value;
        ObseverConstants.OnAttributeValueChanged?.Invoke(type, value);
    }
    
    public void StartGame()
    {
        
    }
    private void SaveGame()
    {
        ConfigDataHelper.HeroData = heroData;
    }
}
