using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponObject weaponObject = null;
    public List<float> CurrentHit = null;
    public void Init()
    {
        CurrentHit.Clear();
        for (int i = 0; i < weaponObject.ListDamage.Count - 1; i++)
        {
            float num = weaponObject.ListDamage[i] * (1 + (0.13f + 0.07f * (int)weaponObject.LevelWeapon)) ;
            //+ [Tarot Card multiplier] + [Fleece multiplier] + [Run Damage multiplier] � [Difficulty modifier]
            CurrentHit.Add(num);
        }
    }
    public virtual void Attack(Vector3 target)
    {

    }
    public float TotalDamage()
    {
        float a = 0;
        foreach (float Damage in CurrentHit)
        {
            a += Damage;
        }
        return a / CurrentHit.Count;
    }
}

