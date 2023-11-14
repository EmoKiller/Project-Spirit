using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDungeonManager : MonoBehaviour
{
    [SerializeField] Image fillAngry;
    [SerializeField] Image iconWeapon;
    [SerializeField] Image iconCurses;
    float currentAngry = 0;
    private void OnEnable()
    {
        EventDispatcher.Addlistener<Sprite>(ListScript.UiDungeonManager,Events.UpdateIconWeapon, UpdateIconWeapon);
        EventDispatcher.Addlistener<Sprite>(ListScript.UiDungeonManager, Events.UpdateIconCurses, UpdateIconCurses);
    }
    private void Start()
    {
        currentAngry = 0;
    }

    private void UpdateAngry(float value)
    {
        currentAngry += value;
    }
    private void UseAngry(float value)
    {
        currentAngry -= value;
    }
    private void UpdateIconWeapon(Sprite spr)
    {
        iconWeapon.sprite = spr;
    }
    private void UpdateIconCurses(Sprite spr)
    {
        iconCurses.sprite = spr;
    }

}
