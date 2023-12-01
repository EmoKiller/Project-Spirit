using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDungeonManager : MonoBehaviour
{
    public enum Script
    {
        UiDungeonManager
    }
    [SerializeField] Image iconWeapon;
    [SerializeField] Image iconCurses;
    private void OnEnable()
    {
        EventDispatcher.Addlistener<Sprite>(Script.UiDungeonManager,Events.UpdateIconWeapon, UpdateIconWeapon);
        EventDispatcher.Addlistener<Sprite>(Script.UiDungeonManager, Events.UpdateIconCurses, UpdateIconCurses);
    }
    private void Start()
    {
        
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
