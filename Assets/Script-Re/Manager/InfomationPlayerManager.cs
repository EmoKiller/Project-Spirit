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
    public float Level
    {
        get { return GetValueAttribute(AttributeType.Level); }
        set
        {
            heroData.attributes[SaveSlot][AttributeType.Level].value = value;
            UpdateValueOf(AttributeType.MaxExpOfLevel, MaxExpPerLevelCurve.Evaluate(GetValueAttribute(AttributeType.Level)));
            CurrentExp = 0;
        }
    }
    public float CurrentExp
    {
        get { return GetValueAttribute(AttributeType.CurrentExp); }
        set
        {
            heroData.attributes[SaveSlot][AttributeType.CurrentExp].value = Mathf.Clamp(value,0, GetValueAttribute(AttributeType.MaxExpOfLevel));
            UpdateValueOf(AttributeType.CurrentExp, GetValueAttribute(AttributeType.CurrentExp));
            if (CurrentExp >= GetValueAttribute(AttributeType.MaxExpOfLevel))
            {
                Level++;
            }
        }
    }
    public float CurrentAngry
    {
        get { return GetValueAttribute(AttributeType.CurrentAngry); }
        set
        {
            heroData.attributes[SaveSlot][AttributeType.CurrentAngry].value = value;
            UpdateValueOf(AttributeType.CurrentAngry, GetValueAttribute(AttributeType.CurrentAngry));
        }

    }
    public float CurrentHunger
    {
        get { return GetValueAttribute(AttributeType.CurrentHunger); }
        set
        {
            heroData.attributes[SaveSlot][AttributeType.CurrentHunger].value = value;
            UpdateValueOf(AttributeType.CurrentHunger, GetValueAttribute(AttributeType.CurrentHunger));
        }
    }
    public float CurrentCoins
    {
        get { return GetValueAttribute(AttributeType.CurrentCoin); }
        set
        {
            heroData.attributes[SaveSlot][AttributeType.CurrentCoin].value = value;
            UpdateValueOf(AttributeType.CurrentCoin, GetValueAttribute(AttributeType.CurrentCoin));
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
        heroData = (HeroData)ConfigDataHelper.HeroData.Clone();
    }
    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        if (PlayerOnScenes() == OnScenes.IntroGame)
        {
            heroData.attributes[SaveSlot][AttributeType.MaxRedHeart].value = 999;
            heroData.attributes[SaveSlot][AttributeType.CurrentRedHeart].value = heroData.attributes[SaveSlot][AttributeType.MaxRedHeart].value;
            return;
        }
        ObseverConstants.OnAttributeValueChanged.AddListener(CheckCurrentRedHeart);
        Level = 1;
        //AttributeOnChange(AttributeType.Level,1);
        //AttributeOnChange(AttributeType.MaxRedHeart, 0);
        //SaveGame();
    }
    public OnScenes PlayerOnScenes()
    {
        return heroData.PlayerOnSceness[SaveSlot];
    }
    public float GetValueAttribute(AttributeType type)
    {
        return heroData.attributes[SaveSlot][type].value;
    }
    [Button]
    public void IncreaseValueOf(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].value += value;
        ObseverConstants.OnIncreaseAttributeValue?.Invoke(type, GetValueAttribute(type));
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
    private void CheckCurrentRedHeart(AttributeType type, float value)
    {
        if (type != AttributeType.CurrentRedHeart)
            return;
        if (heroData.attributes[SaveSlot][AttributeType.CurrentRedHeart].value == 0)
            Debug.Log("PlayerDead");
    }


    public void StartGame()
    {
        
    }
    private void SaveGame()
    {
        ConfigDataHelper.HeroData = heroData;
    }
}
