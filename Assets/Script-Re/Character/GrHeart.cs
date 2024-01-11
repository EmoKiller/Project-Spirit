using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GrHeart
{
    public AttributeType TypeHeart;
    public AttributeType TypeCurrentHeart;
    [SerializeField] private float maxHP = 0;
    public float MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            UpdateHearts();
        }
    }
    [SerializeField] private float currentHP = 0;
    public float CurrentHP
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
    public Action<AttributeType> OnRestoreHeart= null;
    public UnityAction SpecialHeart = null;
    public GrHeart()
    {
        
    }
    public void Add(UIHeart uiHeart)
    {
        heart.Add(uiHeart);
        rectGr.sizeDelta = new Vector2(rectGr.sizeDelta.x + 45, 0);
    }
    public float TakeDamage(ref float valueHit)
    {
        for (int i = heart.Count - 1; i > -1; i--)
        {
            while (heart[i].ReturnCurrent() > 0)
            {
                heart[i].TakeDamage();
                SpecialHeart?.Invoke();
                valueHit--;
                CurrentHP--;
                if (valueHit == 0)
                    break;
            }
            if (valueHit == 0)
                break;
        }
        InfomationPlayerManager.Instance.UpdateValueOf(TypeCurrentHeart, currentHP);
        return valueHit;
    }
    public void RestoreHeart(AttributeType type,float valueRestore)
    {
        if (type != this.TypeCurrentHeart)
            return;
        for (int i = 0; i < heart.Count; i++)
        {
            while (heart[i].ReturnCurrent() < (int)heart[i].heartType)
            {
                heart[i].RestoreHeart();
                valueRestore--;
                CurrentHP++;
                if (valueRestore == 0)
                    break;
            }
            if (valueRestore == 0)
                break;
        }
        InfomationPlayerManager.Instance.UpdateValueOf(TypeCurrentHeart, currentHP);
    }
    public void UpdateHearts()
    {
        rectGr.sizeDelta = new Vector2(0, 0);
        for (int i = 0; i< heart.Count; i++)
        {
            heart[i].Hide();
        }
        heart.Clear();
        CurrentHeart = (int)(maxHP / 2);
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
    public void SetStartMaxCurrentHP(AttributeType type, float maxHP)
    {
        if (type != this.TypeHeart)
            return;
        MaxHP = maxHP;
        CurrentHP = MaxHP;
        InfomationPlayerManager.Instance.UpdateValueOf(TypeCurrentHeart, currentHP);
    }


}
