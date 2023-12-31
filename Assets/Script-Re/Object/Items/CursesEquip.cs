using DG.Tweening;
using System;
using UnityEngine;

public class CursesEquip : MonoBehaviour
{
    [SerializeField] private CursesObject _cursesObject = null;

    public CursesObject CursesObject
    {
        get { return _cursesObject; }
    }
    public Action<Vector3> useSkill = null;
    private bool onUseSkill = false;
    public bool OnUseSkill
    {
        get { return onUseSkill; }
        set { onUseSkill = value; }
    }
    private float timeUseSkill = 1;
    public float TimeUseSkill
    {
        get { return timeUseSkill; }
        set { timeUseSkill = Mathf.Clamp(value, 0, MaxTimeUseSKill); }
    }

    public float DamageSkill => _cursesObject.Damage;
    public float AttackRange => _cursesObject.AttackRange;
    public float SpeedSKill => _cursesObject.Speed;
    public float MaxTimeUseSKill => _cursesObject.TimeUseSKill;
    public float UseAngry => _cursesObject.UseAngry;

    public void Init(TypeCurses type)
    {
        switch (type)
        {
            case TypeCurses.Fireballs:
                CheckCursesFireBall();
                return;
            case TypeCurses.Blasts:
                useSkill = Blasts;
                return;
            case TypeCurses.Slashes:
                useSkill = Slashes;
                return;
            case TypeCurses.Splatters:
                useSkill = Splatters;
                return;
            case TypeCurses.Tentacles:
                useSkill = Tentacles;
                return;
        }
    }
    public void UseSkill(Vector3 foward)
    {
        if (TimeUseSkill == 0)
        {
            InfomationPlayerManager.Instance.CurrentAngry -= CursesObject.UseAngry;
            useSkill?.Invoke(foward);
        }
    }
    private void CheckCursesFireBall()
    {
        switch (CursesObject.TypeNameCurses)
        {
            case NameCurses.FlamingShot:
                useSkill = FlamingShot;
                break;
            case NameCurses.CleansingFire:
                useSkill = CleansingFire;
                break;
            case NameCurses.HoundsOfFate:
                useSkill = HoundsofFate;
                break;
        }
    }
    public void FlamingShot(Vector3 foward)
    {
        InstantiateObjSkill("Fireballs", foward);
    }

    public void CleansingFire(Vector3 foward)
    {
        InstantiateObjSkill("Fireballs", foward);

        Quaternion test = Quaternion.Euler(0, 25, 0);
        Vector3 dir = test * foward;
        InstantiateObjSkill("Fireballs", dir);

        Quaternion test2 = Quaternion.Euler(0, -25, 0);
        Vector3 dir2 = test2 * foward;
        InstantiateObjSkill("Fireballs", dir2);
    }
    public void HoundsofFate(Vector3 foward)
    {
        for (int i = 0; i < CursesObject.multipleObj; i++)
        {
            Quaternion test = Quaternion.Euler(0, UnityEngine.Random.Range(-20,20), 0);
            Vector3 dir = test * foward;
            RewardSystem.Instance.SpawnObjectSkill("HoundsofFate", transform.position, out ObjectSkill outSkill);
            outSkill.transform.rotation = Quaternion.LookRotation(dir);
            outSkill.Init(DamageSkill, SpeedSKill, dir);
        }
        
    }
    public void Blasts(Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkill(CursesObject.TypeNameCurses.ToString(), transform.position, out ObjectSkill outSkill);
        outSkill.Init(DamageSkill, SpeedSKill);
    }
    public void Slashes(Vector3 foward)
    {

    }
    public void Splatters(Vector3 foward)
    {

    }
    public void Tentacles(Vector3 foward)
    {

    }
    private void InstantiateObjSkill(string name,Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkill(name, transform.position, out ObjectSkill outSkill);
        outSkill.Init(DamageSkill, SpeedSKill);
        outSkill.transform.DOMove(transform.position + (foward * AttackRange), SpeedSKill);
    }
    public void CountTime()
    {
        TimeUseSkill -= Time.deltaTime;
    }
    public void ResetTime()
    {
        timeUseSkill = MaxTimeUseSKill;
    }

}
