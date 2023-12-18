using System;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class BaseStartGame
{
    public int Level = 1;
    public int MaxHP = 2;
    public int CurrentHP = 0;
    public int MaxEXPOfLevel = 3;
    public int CurrentExp = 0;
    public float MaxValueAngry = 100;
    public float CurrentAngry = 0;
    public int CurrentCoin = 0;
    public float MaxValueHunger = 100;
    public float CurrentHugner = 100;
    
    public BaseStartGame()
    {

    }
}

