using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] RectTransform lineTop;
    [SerializeField] RectTransform lineBottom;
    [SerializeField] PopUpTalkObject talkList;


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

