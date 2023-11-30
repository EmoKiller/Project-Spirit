using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] RectTransform lineTop;
    [SerializeField] RectTransform lineBottom;
    [SerializeField] TMP_Text showText;
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
    }
    private void Removed()
    {
        gameObject.SetActive(false);
    }
    private void Open()
    {
        lineTop.gameObject.SetActive(true);
        lineBottom.gameObject.SetActive(true);
        lineTop.DOAnchorPos3DY(-38f,2);
        lineBottom.DOAnchorPos3DY(38,2);
    }
    private void Close()
    {
        lineTop.DOAnchorPos3DY(38f, 2);
        lineBottom.DOAnchorPos3DY(-38, 2);
        this.DelayCall(2f, () =>
        {
            lineTop.gameObject.SetActive(false);
            lineBottom.gameObject.SetActive(false);
        });
    }
    
}

