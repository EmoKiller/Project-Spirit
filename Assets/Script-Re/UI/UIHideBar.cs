using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public class UIHideBar : MonoBehaviour
{
    
    [Header("HideBar")]
    [SerializeField] RectTransform wayBlack;
    [SerializeField] GameObject gruopMenuEsc;
    [SerializeField] GameObject inventoryTab;
    [SerializeField] GameObject topBar;
    [SerializeField] GameObject popUp;
    private void Start()
    {
        gruopMenuEsc.SetActive(false);
        inventoryTab.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTabHideBar(inventoryTab, !inventoryTab.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleTabHideBar(gruopMenuEsc, !gruopMenuEsc.activeSelf);
        }
    }
    private void ToggleTabHideBar(GameObject obj,bool value)
    {
        topBar.gameObject.SetActive(!value);
        popUp.gameObject.SetActive(!value);
        if (value == true)
        {

            wayBlack.gameObject.SetActive(value);
            obj.gameObject.SetActive(value);
            wayBlack.DOAnchorPos(new Vector2(0, 0), 0.6f).OnComplete(() =>
            {
                Time.timeScale = 0;
            });
            return;
        }
        Time.timeScale = 1;
        wayBlack.DOAnchorPos(new Vector2(-1200, 0), 0.6f).OnComplete(() =>
        {
            obj.gameObject.SetActive(value);
        });
    }
}
