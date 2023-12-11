using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GrHeart
{
    public EnemGrHeart Type;
    public int maxHP = 0;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            UpdateHearts();
        }
    }
    public int CurrentHeart = 0;
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
    public bool TalkeDamage(ref bool value)
    {
        for (int i = heart.Count - 1; i > -1; i--)
        {
            if (heart[i].ReturnCurrent() > 0)
            {
                heart[i].TakeDamage(1);
                value = true;
                break;
            }
        }
        return value;
    }
    public void UpdateHearts()
    {
        rectGr.sizeDelta = new Vector2(0, 0);
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
        heart.Clear();
        for (int i = 0; i < CurrentHeart; i++)
        {
            CreateNewHeart.Invoke(typeFull);
        }
    }
}
