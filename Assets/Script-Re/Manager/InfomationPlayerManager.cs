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

        EventDispatcher.Addlistener<int>(Script.InfomationPlayerManager, Events.UpdateUICoin, UpdateUICoin);
    }
    private void Start()
    {

    }
    public void Init()
    {
        heroData = (HeroData)ConfigDataHelper.HeroData.Clone();
        heroData.attributes[SaveSlot][AttributeType.Level].OnValueChange = UpdateLevel;
        heroData.attributes[SaveSlot][AttributeType.MaxRedHeart].OnValueChange = UpdateMaxRedHeart;
        heroData.attributes[SaveSlot][AttributeType.CurrentRedHeart].OnValueChange = UpdateCurrentRedHeart;
        heroData.attributes[SaveSlot][AttributeType.MaxRedAddHeart].OnValueChange = UpdateMaxRedAddHeart;
        heroData.attributes[SaveSlot][AttributeType.CurrentRedAddHeart].OnValueChange = UpdateCurrentRedAddHeart;
        heroData.attributes[SaveSlot][AttributeType.MaxBlueHeart].OnValueChange = UpdateMaxBlueHeart;
        heroData.attributes[SaveSlot][AttributeType.CurrentBlueHeart].OnValueChange = UpdateCurrentBlueHeart;
        heroData.attributes[SaveSlot][AttributeType.MaxBlackHeart].OnValueChange = UpdateMaxBlackHeart;
        heroData.attributes[SaveSlot][AttributeType.CurrentBlackHeart].OnValueChange = UpdateCurrentBlackHeart;
        heroData.attributes[SaveSlot][AttributeType.CurrentExp].OnValueChange = UpdateCurrentExp;
        heroData.attributes[SaveSlot][AttributeType.MaxExpOfLevel].OnValueChange = UpdateMaxEXPOfLevel;
        heroData.attributes[SaveSlot][AttributeType.MaxValueAngry].OnValueChange = UpdateMaxValueAngry;
        heroData.attributes[SaveSlot][AttributeType.CurrentAngry].OnValueChange = UpdateCurretAngry;
        heroData.attributes[SaveSlot][AttributeType.CurrentCoin].OnValueChange = UpdateCurrentCoin;
        heroData.attributes[SaveSlot][AttributeType.MaxValueHunger].OnValueChange = UpdateMaxValueHunger;
        heroData.attributes[SaveSlot][AttributeType.CurrentHunger].OnValueChange = UpdateCurrentHunger;
        //SaveGame();
    }
    public float GetValueAtribute(AttributeType type)
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
    }
    /// <summary>
    /// 
    /// </summary>
    public void IncreaseAttributeOnChange(AttributeType type, float value)
    {
        heroData.attributes[SaveSlot][type].OnValueChange?.Invoke(value);
    }
    private void UpdateLevel(float value)
    {
        IncreaseValueOf(AttributeType.Level, value);
        //MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
    }
    private void UpdateCurrentExp(float value)
    {
        IncreaseValueOf(AttributeType.CurrentExp, value);
        if (GetValueAtribute(AttributeType.CurrentExp) >= GetValueAtribute(AttributeType.MaxExpOfLevel))
        {
            IncreaseAttributeOnChange(AttributeType.Level, 1);
            UpdateValueOf(AttributeType.CurrentExp, 0);
        }
        UIManager.Instance.UpdateValueExp();
    }
    private void UpdateMaxEXPOfLevel(float value)
    {
        IncreaseValueOf(AttributeType.MaxExpOfLevel, value);
        UIManager.Instance.SetMaxExpOfLevel();
    }
    private void UpdateMaxRedHeart(float value)
    {
        IncreaseValueOf(AttributeType.MaxRedHeart, value);
        UIManager.Instance.GroupHeart[(int)EnemGrHeart.Red].SetStartMaxCurrentHP((int)GetValueAtribute(AttributeType.MaxRedHeart));
    }
    private void UpdateCurrentRedHeart(float value)
    {
        IncreaseValueOf(AttributeType.CurrentRedHeart, value);
        //MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
    }
    private void UpdateMaxRedAddHeart(float value)
    {
        IncreaseValueOf(AttributeType.MaxRedAddHeart, value);
        UIManager.Instance.GroupHeart[(int)EnemGrHeart.Add].SetStartMaxCurrentHP((int)GetValueAtribute(AttributeType.MaxRedAddHeart));
    }
    private void UpdateCurrentRedAddHeart(float value)
    {
        IncreaseValueOf(AttributeType.CurrentRedAddHeart, value);
        //MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
    }
    private void UpdateMaxBlueHeart(float value)
    {
        IncreaseValueOf(AttributeType.MaxBlueHeart, value);
        UIManager.Instance.GroupHeart[(int)EnemGrHeart.Blue].SetStartMaxCurrentHP((int)GetValueAtribute(AttributeType.MaxBlueHeart));
    }
    private void UpdateCurrentBlueHeart(float value)
    {
        IncreaseValueOf(AttributeType.CurrentBlueHeart, value);
        //MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
    }
    private void UpdateMaxBlackHeart(float value)
    {
        IncreaseValueOf(AttributeType.MaxBlackHeart, value);
        UIManager.Instance.GroupHeart[(int)EnemGrHeart.Black].SetStartMaxCurrentHP((int)GetValueAtribute(AttributeType.MaxBlackHeart));
    }
    private void UpdateCurrentBlackHeart(float value)
    {
        IncreaseValueOf(AttributeType.CurrentBlackHeart, value);
        //MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
    }
    private void UpdateMaxValueAngry(float value)
    {
        IncreaseValueOf(AttributeType.MaxValueAngry, value);
    }
    private void UpdateCurretAngry(float value)
    {
        IncreaseValueOf(AttributeType.CurrentAngry, value);
    }
    private void UpdateCurrentCoin(float value)
    {
        IncreaseValueOf(AttributeType.CurrentCoin, value);
    }
    private void UpdateMaxValueHunger(float value)
    {
        IncreaseValueOf(AttributeType.MaxValueHunger, value);
    }
    private void UpdateCurrentHunger(float value)
    {
        IncreaseValueOf(AttributeType.CurrentHunger, value);
    }

    public void StartGame()
    {
        
    }
    private void UpdateUICoin(int Value)
    {
        heroData.attributes[SaveSlot][AttributeType.CurrentCoin].value += Value;
    }
    private void SaveGame()
    {
        ConfigDataHelper.HeroData = heroData;
    }
}
