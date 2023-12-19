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
    public HeroData HeroData
    {
        get
        {
            if (heroData == null)
                heroData = (HeroData)ConfigDataHelper.HeroData.Clone();
            return heroData;
        }
    }
    [SerializeField] private AnimationCurve MaxExpPerLevelCurve;

    public int CurrentHP
    {
        get { return (int)HeroData.attributes[SaveSlot][AttributeType.CurrentHP].value; }
    }
    private int Level
    {
        get { return (int)HeroData.attributes[SaveSlot][AttributeType.Level].value; }
        set
        {
            HeroData.attributes[SaveSlot][AttributeType.Level].value = value;
            MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
        }
    }
    public int MaxEXPOfLevel
    {
        get { return (int)HeroData.attributes[SaveSlot][AttributeType.MaxExpOfLevel].value; }
        set
        {
            HeroData.attributes[SaveSlot][AttributeType.MaxExpOfLevel].value = value;
            UIManager.Instance.SetMaxExpOfLevel();
        }
    }
    public int CurrnetExp
    {
        get { return (int)HeroData.attributes[SaveSlot][AttributeType.CurrentExp].value; }
        set
        {
            HeroData.attributes[SaveSlot][AttributeType.CurrentExp].value = Math.Clamp(value, 0, MaxEXPOfLevel);
        }
    }
    public float MaxValueAngry
    {
        get { return HeroData.attributes[SaveSlot][AttributeType.MaxValueAngry].value; }
        set { }
    }
    public float CurretAngry
    {
        get { return HeroData.attributes[SaveSlot][AttributeType.CurrentAngry].value; }
        set
        {
            HeroData.attributes[SaveSlot][AttributeType.CurrentAngry].value = Math.Clamp(value, 0, MaxValueAngry);
            UIManager.Instance.UpdateValueAngry();
        }
    }
    public int CurrentCoin
    {
        get { return (int)HeroData.attributes[SaveSlot][AttributeType.CurrentCoin].value; }
    }
    public float MaxValueHunger
    {
        get { return HeroData.attributes[SaveSlot][AttributeType.MaxValueHunger].value; }
        set { }
    }
    public float CurrentHunger
    {
        get { return HeroData.attributes[SaveSlot][AttributeType.CurrentHunger].value; }
        set
        {
            HeroData.attributes[SaveSlot][AttributeType.CurrentHunger].value = Math.Clamp(value, 0, MaxValueHunger);
            UIManager.Instance.UpdateValueHunger();
        }
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        EventDispatcher.Addlistener<int>(Script.InfomationPlayerManager, Events.UpdateUICoin, UpdateUICoin);
        heroData = (HeroData)ConfigDataHelper.HeroData.Clone();

    }
    private void Start()
    {
        Level = 1;


    }
    public void Init()
    {
        heroData.attributes[SaveSlot][AttributeType.CurrentExp].OnValueChange = UpdateCurrentExp;
    }
    public float GetValueAtribute(AttributeType type)
    {
        return HeroData.attributes[SaveSlot][type].value;
    }
    public void IncreaseAttribute(AttributeType type, float value)
    {
        HeroData.attributes[SaveSlot][type].OnValueChange?.Invoke(value);
    }
    private void IncreaseValueOf(AttributeType type, float value)
    {
        HeroData.attributes[SaveSlot][type].value += value;
    }
    private void UpdateValueOf(AttributeType type, float value)
    {
        HeroData.attributes[SaveSlot][type].value = value;
    }
    //private void CurrentHP()
    //{

    //}

    private void UpdateCurrentExp(float value)
    {
        IncreaseValueOf(AttributeType.CurrentExp, value);
        if (CurrnetExp >= MaxEXPOfLevel)
        {
            Level++;
            UpdateValueOf(AttributeType.CurrentExp, 0);
        }
        UIManager.Instance.UpdateValueExp();
    }
    public void StartGame()
    {
        Level = 1;
    }
    private void UpdateUICoin(int Value)
    {
        heroData.attributes[SaveSlot][AttributeType.CurrentCoin].value += Value;
    }
    private void SaveGame()
    {
        ConfigDataHelper.HeroData = HeroData;
    }
}
