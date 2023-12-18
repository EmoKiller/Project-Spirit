using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfomationPlayerManager : MonoBehaviour
{
    public enum Script
    {
        InfomationPlayerManager
    }
    public static InfomationPlayerManager Instance = null;
    [SerializeField] BaseStartGame DataNewGame = null;
    [SerializeField] private AnimationCurve MaxExpPerLevelCurve;
    public int MaxHP
    {
        get { return DataNewGame.MaxHP; }
        set { DataNewGame.MaxHP = value; }
    }
    public int CurrentHP
    {
        get { return DataNewGame.CurrentHP; }
        set { DataNewGame.CurrentHP = value; }
    }
    public int Level
    {
        get { return DataNewGame.Level; }
        set
        {
            DataNewGame.Level = value;
            MaxEXPOfLevel = Mathf.RoundToInt(MaxExpPerLevelCurve.Evaluate(Level));
        }
    }
    public int MaxEXPOfLevel
    {
        get { return DataNewGame.MaxEXPOfLevel; }
        set
        {
            DataNewGame.MaxEXPOfLevel = value;
            UIManager.Instance.SetMaxExpOfLevel();
        }
    }
    public int CurrnetExp
    {
        get { return DataNewGame.CurrentExp; }
        set
        {
            DataNewGame.CurrentExp = Math.Clamp(value, 0, MaxEXPOfLevel);
            if (CurrnetExp >= MaxEXPOfLevel)
            {
                Level++;
                DataNewGame.CurrentExp = 0;
            }
            UIManager.Instance.UpdateValueExp();
        }
    }
    public float MaxValueAngry
    {
        get { return DataNewGame.MaxValueAngry; }
        set { }
    }
    public float CurretAngry
    {
        get { return DataNewGame.CurrentAngry; }
        set
        {
            DataNewGame.CurrentAngry = Math.Clamp(value, 0, MaxValueAngry);
            UIManager.Instance.UpdateValueAngry();
        }
    }
    public int CurrentCoin
    {
        get { return DataNewGame.CurrentCoin; }
    }
    public float MaxValueHunger
    {
        get { return DataNewGame.MaxValueHunger; }
        set { }
    }
    public float CurrentHunger
    {
        get { return DataNewGame.CurrentHugner; }
        set
        {
            DataNewGame.CurrentHugner = Math.Clamp(value, 0, MaxValueHunger);
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
        
    }
    private void Start()
    {
        DataNewGame = ConfigDataHelper.BaseStartGame;
    }
    public void Init()
    {
        
    }
    private void UpdateUICoin(int Value)
    {
        DataNewGame.CurrentCoin += Value;
    }
    private void SaveGame()
    {
        ConfigDataHelper.BaseStartGame = DataNewGame;
    }
}
