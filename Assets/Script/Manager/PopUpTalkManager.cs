using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTalkManager : MonoBehaviour
{
    [SerializeField] RectTransform top;
    [SerializeField] RectTransform bottom;
    [SerializeField] TMP_Text showText;
    [SerializeField] GameObject boxTalk;
    [SerializeField] GameObject uiSelect;
    [SerializeField] GameObject infoWeapon;
    private void Start()
    {
        gameObject.SetActive(false);
        EventDispatcher.Addlistener(ListScript.PopUpTalkManager, Events.AddListener, AddListener);
        EventDispatcher.Addlistener(ListScript.PopUpTalkManager, Events.RemoveEvent, Removed);
    }
    private void AddListener()
    {
        gameObject.SetActive(true);
        EventDispatcher.Addlistener(ListScript.PopUpTalkManager, Events.Open, Open);
        EventDispatcher.Addlistener(ListScript.PopUpTalkManager, Events.Close, Close);
        EventDispatcher.Addlistener<string>(ListScript.PopUpTalkManager, Events.OpenPopup, OpenBoxTalk);
        EventDispatcher.Addlistener(ListScript.PopUpTalkManager, Events.ClosePopup, CloseBoxTalk);
        EventDispatcher.Addlistener<bool>(ListScript.PopUpTalkManager, Events.UiSelect, SetUiSelect);
        EventDispatcher.Addlistener<bool>(ListScript.PopUpTalkManager, Events.SetInfoWeapon, SetInfoWeapon);
    }
    private void Removed()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && infoWeapon.activeSelf == false)
        {
            EventDispatcher.Publish(ListScript.PopUpTalk, Events.OpenPopup);

        }
    }
    private void SetInfoWeapon(bool value)
    {
        infoWeapon.SetActive(value);
    }
    private void SetUiSelect(bool value)
    {
        uiSelect.SetActive(value);
    }
    private void OpenBoxTalk(string text)
    {
        boxTalk.SetActive(true);
        showText.text = text;
    }
    private void CloseBoxTalk()
    {
        boxTalk.SetActive(false);
    }
    private void Open()
    {
        top.gameObject.SetActive(true);
        bottom.gameObject.SetActive(true);
        top.DOAnchorPos3DY(-38f,2);
        bottom.DOAnchorPos3DY(38,2);
    }
    private void Close()
    {
        top.DOAnchorPos3DY(38f, 2);
        bottom.DOAnchorPos3DY(-38, 2);
        this.DelayCall(2f, () =>
        {
            top.gameObject.SetActive(false);
            bottom.gameObject.SetActive(false);
        });
    }
    
}

