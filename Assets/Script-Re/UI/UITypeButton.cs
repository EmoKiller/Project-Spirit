using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITypeButton : MonoBehaviour
{
    public TypeUIButton Type;
    private Image _img = null;
    public Image Img
    {
        get 
        {
            if (_img == null)
            {
                List<Image> list = GetComponentsInChildren<Image>().ToList();
                foreach (var items in list)
                {
                    if (items.type == Image.Type.Filled)
                        _img = items;
                }
            }
            return _img; 
        }
    }
    public float FillAmount
    {
        get
        {
            return Img.fillAmount;
        }
        set
        {
            _img.fillAmount = value;
        }
    }
    private TMP_Text _text = null;
    public TMP_Text text 
    {
        get
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();
            return _text;
        }
    }
    public string Text 
    {
        get
        { 
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }


}
