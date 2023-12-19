using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICard : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
{
    [SerializeField] private TMP_Text _nameCard;
    public string NameCard
    {
        get { return _nameCard.text; }
        set { _nameCard.text = value; }
    }
    [SerializeField] private TMP_Text _quoteCard;
    public string QuoteCard
    {
        get { return _quoteCard.text; }
        set { _quoteCard.text = value; }
    }
    [SerializeField] private TMP_Text _descriptionCard;
    public string DescriptionCard
    {
        get { return _descriptionCard.text; }
        set { _descriptionCard.text = value; }
    }
    [SerializeField] private Image _CradFont;
    public Sprite CradFontSprite
    {
        get { return _CradFont.sprite; }
        set { _CradFont.sprite = value; }
    }
    [SerializeField] private GameObject _gameObjectCard;
    public Action OnActiveCard = null;

    private Sequence sequenceLoop;
    private void Awake()
    {
        sequenceLoop = DOTween.Sequence();
    }
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, -90, 0), 0.8f,RotateMode.Fast).OnComplete(() =>
        {
            _CradFont.enabled = enabled;
            transform.DORotate(new Vector3(0, 0, 0), 0.8f);
        });
    }
    private void OnDisable()
    {
        transform.eulerAngles = Vector3.zero;
        _CradFont.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _gameObjectCard.transform.DOScale(new Vector3(1.2f,1.2f,1),0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _gameObjectCard.transform.DOScale(new Vector3(1, 1, 0), 0.5f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //transform.DOScale(new Vector3(0.3f,0.3f,0),0.8f);
        //transform.DOLocalMoveX(2,0.8f);
        OnActiveCard?.Invoke();


    }

    
}
