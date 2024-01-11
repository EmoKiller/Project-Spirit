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
    public int price;
    public List<Image> Check = new List<Image>();
    [SerializeField] private Button buttonAttribute;
    public Button ButtonAttribute => this.TryGetMonoComponent(ref buttonAttribute);
    public Action<AttributeType, Sprite, string, float, string, int> OnClickAttribute = null;
    private void Awake()
    {
        ButtonAttribute.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        OnClickAttribute?.Invoke(AttributeAdded, Sprite, Name, valueAdded, quote, price);
    }
}
