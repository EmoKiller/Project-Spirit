using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CharacterAnimator;

public class SkillsPlayer : MonoBehaviour
{
    [SerializeField] CursesEquip _CrusesEquip = null;
    public CursesEquip CrusesEquip
    {
        get { return _CrusesEquip; }
        set
        { 
            _CrusesEquip = value;
            Init(_CrusesEquip.CursesObject.TypeCurses);
        }
    }
    [SerializeField] private Transform aimingRecticule;
    [SerializeField] private Transform fillAimingRecticule;
    [SerializeField] private GameObject aiming;
    public Action<Vector3> useSkill = null;
    [SerializeField] private bool onUseSkill = false;
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
    public string TypeNameCurses => _CrusesEquip.CursesObject.TypeNameCurses.ToString();
    public float DamageSkill => _CrusesEquip.CursesObject.Damage;
    public float AttackRange => _CrusesEquip.CursesObject.AttackRange;
    public float SpeedSKill => _CrusesEquip.CursesObject.Speed;
    public float MaxTimeUseSKill => _CrusesEquip.CursesObject.TimeUseSKill;
    public float UseAngry => _CrusesEquip.CursesObject.UseAngry;
    private void Update()
    {
        if(_CrusesEquip == null
            || !GetCurrentAngryCanUseSkill() 
            || Player.Instance.CheckAnimationStates(AnimationStates.Attack)
            || Player.Instance.CheckAnimationStates(AnimationStates.Rolling))
            return;
        if (Input.GetMouseButton(1) && GetCurrentAngryCanUseSkill())
        {
            Debug.Log("UseSkill");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.Instance.Rolling();
                ResetTime();
                return;
            }
            OnUseSkill = true;
            CountTime();
            fillAimingRecticule.transform.localScale = new Vector3(TimeUseSkill, 1, 1);
            GameUtilities.ScreenRayCastOnWorld(AimingRecticule);
            Player.Instance.CharacterAni.SetTrigger("UseSkill");
            if (CrusesEquip.CursesObject.TypeCurses == TypeCurses.Fireballs)
            {
                aiming.gameObject.SetActive(true);
            }
            return;
        }
        if (Input.GetMouseButtonUp(1))
        {
            UseSkill(Player.Instance.GetDirection());
            Player.Instance.CharacterAni.SetTrigger("Idie");
            Player.Instance.StopAni();
            ResetTime();
            
            return;
        }
    }
    protected void AimingRecticule(Vector3 targetPos)
    {
        Player.Instance.Direction.position = transform.position + (targetPos - transform.position).normalized;
        aimingRecticule.DORotateQuaternion(Quaternion.LookRotation((targetPos - transform.position).normalized, Vector3.up), 0.3f);
    }
    private bool GetCurrentAngryCanUseSkill()
    {
        return InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CurrentAngry) > UseAngry;
    }
    public void Init(TypeCurses type)
    {
        switch (type)
        {
            case TypeCurses.Fireballs:
                CheckCursesFireBall();
                break;
            case TypeCurses.Blasts:
                useSkill = Blasts;
                break;
            case TypeCurses.Slashes:
                useSkill = Slashes;
                break;
            case TypeCurses.Splatters:
                useSkill = Splatters;
                break;
            case TypeCurses.Tentacles:
                useSkill = Tentacles;
                break;
        }
    }
    public void UseSkill(Vector3 foward)
    {
        if (TimeUseSkill == 0)
        {
            InfomationPlayerManager.Instance.CurrentAngry -= _CrusesEquip.CursesObject.UseAngry;
            useSkill?.Invoke(foward);
        }
    }
    private void CheckCursesFireBall()
    {
        switch (_CrusesEquip.CursesObject.TypeNameCurses)
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

        Quaternion test = Quaternion.Euler(0, 10, 0);
        Vector3 dir = test * foward;
        InstantiateObjSkill("Fireballs", dir);

        Quaternion test2 = Quaternion.Euler(0, -10, 0);
        Vector3 dir2 = test2 * foward;
        InstantiateObjSkill("Fireballs", dir2);
    }
    public void HoundsofFate(Vector3 foward)
    {
        for (int i = 0; i < _CrusesEquip.CursesObject.multipleObj; i++)
        {
            Quaternion test = Quaternion.Euler(0, UnityEngine.Random.Range(-20, 20), 0);
            Vector3 dir = test * foward;
            RewardSystem.Instance.SpawnObjectSkill("HoundsofFate", transform.position, out ObjectSkill outSkill);
            outSkill.transform.rotation = Quaternion.LookRotation(dir);
            outSkill.Init(DamageSkill, SpeedSKill, dir);
        }

    }
    public void Blasts(Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkill(TypeNameCurses, transform.position, out ObjectSkill outSkill);
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
    private void InstantiateObjSkill(string name, Vector3 foward)
    {
        RewardSystem.Instance.SpawnObjectSkill(name, transform.position + new Vector3(0, 1.5f, 0), out ObjectSkill outSkill);
        outSkill.Init(DamageSkill, SpeedSKill);
        outSkill.transform.DOMove(transform.position + (foward.normalized * AttackRange) + new Vector3(0, 1.5f, 0), SpeedSKill);
    }
    public void CountTime()
    {
        TimeUseSkill -= Time.deltaTime;
    }
    public void ResetTime()
    {
        timeUseSkill = MaxTimeUseSKill;
        OnUseSkill = false;
        aiming.gameObject.SetActive(false);
    }
}
