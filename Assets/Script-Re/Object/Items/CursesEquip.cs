using DG.Tweening;
using System;
using UnityEngine;

public class CursesEquip : MonoBehaviour
{
    [SerializeField] private CursesObject cursesObject = null;

    public CursesObject CursesObject
    {
        get { return cursesObject; }
    }
}
