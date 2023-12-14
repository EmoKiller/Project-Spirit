using System;
using UnityEngine;

public class CursesEquip : MonoBehaviour
{
    public CursesObject CursesObject = null;
    Action useSkill = null;
    public void Init(TypeCurses type)
    {
        switch (type)
        {
            case TypeCurses.Fireballs:
                return;
            case TypeCurses.Blasts:
                return;
            case TypeCurses.Slashes:
                return;
            case TypeCurses.Splatters:
                return;
            case TypeCurses.Tentacles:
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
