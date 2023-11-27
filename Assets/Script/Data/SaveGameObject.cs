using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSaveGame", menuName = "GameUtilities/SaveGame")]
public class SaveGameObject : ScriptableObject
{
    [Header("Comfinguration")]
    public int level = 0;
    private int ExpMaxOfLevel = 0;
    public int CurrentExp = 0;
    public int AmountFollowers = 0;
    public int AmountOfCoin = 0;
    [Header("Comfinguration")]
    public Action<int> AmountOfCoinAction = null;
    public SaveGameObject()
    {
        AmountOfCoinAction = UpdateAmountOfCoin;
    }
    public void ShowInfomation()
    {
        Debug.Log("Level: " + level);
        Debug.Log("Current Exp: " + CurrentExp);
        Debug.Log("AmountFollowers: " + AmountFollowers);
        Debug.Log("AmountOfCoin: " + AmountOfCoin);
    }
    public void Default()
    {
        level = 0;
        CurrentExp = 0;
        AmountFollowers = 0;
        AmountOfCoin = 0;
    }
    private void UpdateLevel()
    {
        level++;
    }
    public void UpdateAmountFollowers()
    {
        AmountFollowers ++;
    }
    public void UpdateAmountOfCoin(int value)
    {
        AmountOfCoin += value;
    }
    public void UpdateExp(int value)
    {
        CurrentExp += value;
        if (CurrentExp >= ExpMaxOfLevel)
        {
            UpdateLevel();
        }
    }
    
}
