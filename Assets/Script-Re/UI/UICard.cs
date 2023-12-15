using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICard : MonoBehaviour
{
    [SerializeField] private Image CradBack;
    [SerializeField] private GameObject cardInfo;
    public Sprite CradBackSprite
    {
        get
        {
            return CradBack.sprite;
        }
        set
        {
            CradBack.sprite = value;
        }
    }
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, -90, 0), 0.8f).OnComplete(() =>
        {
            CradBack.enabled = enabled;
            transform.DORotate(new Vector3(0, 0, 0), 0.8f);
        });
    }
    private void OnDisable()
    {
        transform.eulerAngles = Vector3.zero;
        CradBack.enabled = false;
    }
}
