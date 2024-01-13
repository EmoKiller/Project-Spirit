using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributePowerUP : MonoBehaviour
{
    public AttributeType AttributeAdded;
    [SerializeField] Image _image;
    public Sprite Sprite
    {
        get { return _image.sprite; }
    }
    public string Name;
    public float valueAdded;
    public string quote;
    [SerializeField]private int price = 20;
    public int Price => price * numberTick;
    public List<Image> Check = new List<Image>();
    private int numberTick = 1;
    public int NumberTick => numberTick - 1;
    [SerializeField] private Button buttonAttribute;
    public Button ButtonAttribute => this.TryGetMonoComponent(ref buttonAttribute);
    public Action<AttributeType, Sprite, string, float, string, int> OnClickAttribute = null;
    public Action<Sprite> ActiveTick = null;
    private void Awake()
    {
        ButtonAttribute.onClick.AddListener(OnClick);
        ActiveTick = AddTick;
    }
    private void OnClick()
    {
        OnClickAttribute?.Invoke(AttributeAdded, Sprite, Name, valueAdded, quote, Price);
    }
    public void AddTick(Sprite spr)
    {
        Check[NumberTick].sprite = spr;
        numberTick++;
    }
}
