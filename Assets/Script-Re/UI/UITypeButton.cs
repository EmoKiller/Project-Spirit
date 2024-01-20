using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITypeButton : MonoBehaviour
{
    public TypeUIButton Type;
    private Image _img = null;
    private Image Img
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
        get { return Img.fillAmount; }
        set { _img.fillAmount = value; }
    }
    private TMP_Text _text = null;
    private TMP_Text text
    {
        get => this.TryGetMonoComponent(ref _text);
    }
    public string Text
    {
        get { return text.text; }
        set { text.text = value; }
    }
}
