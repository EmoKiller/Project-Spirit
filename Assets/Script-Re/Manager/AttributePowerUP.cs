using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributePowerUP : MonoBehaviour
{
    public ShopPowerAttributes TypePower;
    public AttributeType AttributeAdded;
    [SerializeField] Image _image;
    public Sprite Sprite
    {
        get { return _image.sprite; }
    }
    public string Name;
    public float valueAdded;
    public string quote;
    [SerializeField] private float price = 20;
    public float Price => price * numberTick;
    public List<Image> Check = new List<Image>();
    [SerializeField] private float numberTick = 1;
    public float NumberTick => numberTick - 1;
    [SerializeField] private Button buttonAttribute;
    public Button ButtonAttribute => this.TryGetMonoComponent(ref buttonAttribute);
    public Action<AttributeType, Sprite, string, float, string, float> OnClickAttribute = null;
    public Action<Sprite> ActiveTick = null;
    private void Awake()
    {
        ButtonAttribute.onClick.AddListener(OnClick);
        ActiveTick = AddTick;
        UpdateInfomation();
    }
    private void Start()
    {
        //Check[0].sprite = ObjectPooling.Instance.SpriteAtlasItems.GetSprite("Checkmark");
        //Check[0].sprite = ObjectPooling.Instance.SpriteAtlasItems.GetSprite("Combat_TarotCardShrine_0");
        
    }
    private void UpdateInfomation()
    {
        BaseShopPowerAddattributes item = ConfigDataHelper.GetValueBaseShopPowerAddattributes(TypePower);
        Name = item.name;
        AttributeAdded = item.AttributeAdded;
        valueAdded = item.valueAdded;
        quote = item.quote;
        price = item.price;
        numberTick = item.numberTick;
    }
    public void Init()
    {
        numberTick = InfomationPlayerManager.Instance.GetValuePowerUpbought(TypePower).numberTick;
        for (int i = 0; i < numberTick - 1; i++)
        {
            Check[i].sprite = ObjectPooling.Instance.SpriteAtlasItems.GetSprite("Combat_TarotCardShrine_0");
        }
    }
    private void OnClick()
    {
        OnClickAttribute?.Invoke(AttributeAdded, Sprite, Name, valueAdded, quote, Price);
    }
    public void AddTick(Sprite spr)
    {
        Check[(int)NumberTick].sprite = spr;
        numberTick++;
    }
}
