using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

[System.Serializable]
public class GrHeart
{
    public EnemGrHeart Type;
    [SerializeField] private int maxHP = 0;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            UpdateHearts();
        }
    }
    [SerializeField] private int currentHP = 0;
    public int CurrentHP
    {
        get { return currentHP; }
        set
        {
            currentHP = Math.Clamp(value, 0, maxHP);
        }
    }
    [SerializeField] private int CurrentHeart = 0;
    public RectTransform rectGr = null;
    public List<UIHeart> heart = null;
    public EnemGrPriteHeart typeFull;
    public EnemGrPriteHeart typeHalf;
    public Action<EnemGrPriteHeart> CreateNewHeart = null;
    public GrHeart()
    {

    }
    public void Add(UIHeart uiHeart)
    {
        heart.Add(uiHeart);
        rectGr.sizeDelta = new Vector2(rectGr.sizeDelta.x + 45, 0);
    }
    public int TalkeDamage(ref int valueHit)
    {
        for (int i = heart.Count - 1; i > -1; i--)
        {
            while (heart[i].ReturnCurrent() > 0)
            {
                heart[i].TakeDamage(1);
                valueHit--;
                CurrentHP--;
                if (valueHit == 0)
                    break;
            }
            if (valueHit == 0)
                break;
        }
        return valueHit;
    }
    public void RestoreHeart(int valueRestore)
    {
        for (int i = 0; i < heart.Count; i++)
        {
            while (heart[i].ReturnCurrent() < (int)heart[i].heartType)
            {
                heart[i].RestoreHeart(1);
                valueRestore--;
                CurrentHP++;
                if (valueRestore == 0)
                    break;
            }
            if (valueRestore == 0)
                break;
        }
    }
    public void UpdateHearts()
    {
        rectGr.sizeDelta = new Vector2(0, 0);
        foreach (UIHeart item in heart)
            item.Hide();
        heart.Clear();
        CurrentHeart = maxHP / 2;
        if (maxHP % 2 == 0)
            UpdateHeart();
        else
        {
            UpdateHeart();
            CreateNewHeart.Invoke(typeHalf);
        }
    }
    public void UpdateHeart()
    {
        for (int i = 0; i < CurrentHeart; i++)
        {
            CreateNewHeart.Invoke(typeFull);
        }
    }
    public void SetStartMaxCurrentHP(int maxHP)
    {
        MaxHP = maxHP;
        CurrentHP = MaxHP;
    }
    public void AddHeartAndRestoreFull(int value)
    {
        MaxHP += value;
        CurrentHP = MaxHP;
    }
    public bool CheckCurrentHP()
    {
        return CurrentHP == MaxHP;
    }


}
