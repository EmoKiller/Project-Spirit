using DG.Tweening;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] RectTransform WayBlack;
    [SerializeField] GameObject WayWhite;
    bool ToggleValue = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTab();
        }
    }
    private void ToggleTab()
    {
        ToggleValue = !ToggleValue;
        
        if (ToggleValue)
        {
            WayBlack.gameObject.SetActive(ToggleValue);
            WayBlack.DOAnchorPos(new Vector2(550,0),1);
        }else
            WayBlack.DOAnchorPos(new Vector2(-550,0), 1).OnComplete(() =>
            {
                WayBlack.gameObject.SetActive(ToggleValue);
            });
    }
}
