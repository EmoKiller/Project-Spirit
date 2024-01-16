using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class PowerUP : MonoBehaviour
{
    
    [SerializeField] List<AttributePowerUP> attributePowerUPs;
    public List<AttributePowerUP> AttributePowerUPs { get {  return attributePowerUPs; } }
    [SerializeField] GameObject popupShow;
    [SerializeField] Sprite sprActive;
    [Header("Show")]
    [SerializeField] TMP_Text namePower;
    [SerializeField] Image showImage;
    [SerializeField] TMP_Text quoteShow;
    [SerializeField] TMP_Text priceText;
    [SerializeField] float valueAdded;
    [SerializeField] private float price;
    private AttributeType attributeAdded;
    [Header("ButtonBuy")]
    [SerializeField] Button showButton;
    public string NamePowerText
    {
        get { return namePower.text; }
        set { namePower.text = value; }
    }
    public string PriceText
    {
        get { return priceText.text; }
        set { priceText.text = value; }
    }
    public Sprite SpriteImage
    {
        get { return showImage.sprite; }
        set { showImage.sprite = value; }
    }
    public float ValueAdded => valueAdded;
    public float Price => price;
    public AttributeType AttributeAdded => attributeAdded;
    public GameObject PopupShow => popupShow;
    public Button ShowButton => showButton;
    public Sprite SprActive => sprActive;

    public void Init()
    {
        foreach (var attribute in attributePowerUPs)
        {
            attribute.OnClickAttribute = OnClickPowerUp;
        }
    }
    private void Start()
    {
        popupShow.SetActive(false);
    }
    private void OnDisable()
    {
        popupShow.SetActive(false);
    }
    private void OnClickPowerUp(AttributeType attributeAdded, Sprite sprite, string name, float valueAdded, string quote , float price )
    {
        this.attributeAdded = attributeAdded;
        SpriteImage = sprite;
        NamePowerText = name;
        this.valueAdded = valueAdded;
        quoteShow.text = quote;
        this.price = price;
        PriceText = price.ToString();
        popupShow.SetActive(true);
    }

    


}
