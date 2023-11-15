using DG.Tweening;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] RectTransform WayBlack;
    [SerializeField] GameObject GruopMenuEsc;
    [SerializeField] GameObject InventoryTab;
    bool ToggleValue = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTab();
            //InventoryTab
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //GruopMenuEsc
        }
    }
    private void ToggleTab()
    {
        ToggleValue = !ToggleValue;
        
        if (ToggleValue)
        {
            WayBlack.gameObject.SetActive(ToggleValue);
            WayBlack.DOAnchorPos(new Vector2(0,0),1);
        }else
            WayBlack.DOAnchorPos(new Vector2(-1200,0), 1).OnComplete(() =>
            {
                WayBlack.gameObject.SetActive(ToggleValue);
            });
    }
}
