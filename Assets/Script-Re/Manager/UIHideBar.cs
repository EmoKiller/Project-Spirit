using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIHideBar : MonoBehaviour
{
    
    [Header("HideBar")]
    [SerializeField] RectTransform WayBlack;
    [SerializeField] GameObject GruopMenuEsc;
    [SerializeField] GameObject InventoryTab;
    bool _isOnTab = false;
    public bool IsOnTab
    {
        get { return _isOnTab; }
        set 
        {
            _isOnTab = value;
            ToggleTabHideBar(value);
        }
    }
    
    private void Start()
    {
        GruopMenuEsc.SetActive(false);
        InventoryTab.SetActive(false);
    }
    private void ToggleTabHideBar(bool value)
    {
        if (_isOnTab)
        {
            WayBlack.gameObject.SetActive(_isOnTab);
            WayBlack.DOAnchorPos(new Vector2(0, 0), 1);
            InventoryTab.gameObject.SetActive(_isOnTab);
            return;
        }
        WayBlack.DOAnchorPos(new Vector2(-1200, 0), 1).OnComplete(() =>
        {
            WayBlack.gameObject.SetActive(_isOnTab);
        });
    }
}
