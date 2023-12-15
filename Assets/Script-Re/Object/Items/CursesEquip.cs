using System;
using UnityEngine;

public class CursesEquip : MonoBehaviour
{
    public CursesObject CursesObject = null;
    public Action useSkill = null;
    public void Init(TypeCurses type)
    {
        switch (type)
        {
            case TypeCurses.Fireballs:
                useSkill = FireBalls;
                Debug.Log(TypeCurses.Fireballs);
                return;
            case TypeCurses.Blasts:
                useSkill = Blasts;
                Debug.Log(TypeCurses.Blasts);
                return;
            case TypeCurses.Slashes:
                useSkill = Slashes;
                Debug.Log(TypeCurses.Slashes);
                return;
            case TypeCurses.Splatters:
                useSkill = Splatters;
                Debug.Log(TypeCurses.Splatters);
                return;
            case TypeCurses.Tentacles:
                useSkill = Tentacles;
                Debug.Log(TypeCurses.Tentacles);
                return;
        }
    }
    public void UseSkill()
    {
        useSkill?.Invoke();
    }
    public void FireBalls()
    {

    }
    public void Blasts()
    {

    }
    public void Slashes()
    {

    }
    public void Splatters()
    {

    }
    public void Tentacles()
    {

    }
}
