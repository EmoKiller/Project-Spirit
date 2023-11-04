using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkTime : MonoBehaviour
{
    [SerializeField] RectTransform top;
    [SerializeField] RectTransform bottom;
    [SerializeField] TMP_Text showText;
    [SerializeField] GameObject boxTalk;

    private void OnEnable()
    {
        EventDispatcher.Addlistener(ListScript.TalkTime, Events.Open, Open);
        EventDispatcher.Addlistener(ListScript.TalkTime, Events.Close, Close);
        EventDispatcher.Addlistener<string>(ListScript.TalkTime, Events.OpenTalkBox, OpenBoxTalk);
    }
    private void OpenBoxTalk(string text)
    {
        boxTalk.SetActive(true);
        showText.text = text;
    }
    private void Open()
    {
        top.DOAnchorPos3DY(-38f,2);
        bottom.DOAnchorPos3DY(38,2);
    }
    private void Close()
    {
        boxTalk.SetActive(false);
        top.DOAnchorPos3DY(38f, 2);
        bottom.DOAnchorPos3DY(-38, 2);
    }
}

