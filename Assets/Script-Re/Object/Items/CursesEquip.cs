using DG.Tweening;
using System;
using UnityEngine;

public class CursesEquip : MonoBehaviour
{
    [SerializeField] private CursesObject cursesObject = null;
    [SerializeField] CursesPodium cursesPodium = null;
    public CursesObject CursesObject
    {
        get { return cursesObject; }
    }
    public void SetBoxCollider(bool value)
    {
        cursesPodium.SetBoxCollider(value);
    }
}
