using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITypeButton : MonoBehaviour
{
    public TypeUIButton Type;
    private Image img = null;
    public float FillAmount
    {
        get
        {
            if (img == null)
            {
                List<Image> list = GetComponentsInChildren<Image>().ToList();
                foreach (var items in list)
                {
                    if (items.type == Image.Type.Filled)
                        img = items;
                }
            }
            return img.fillAmount;
        }
        set
        {
            img.fillAmount = value;
        }
    }
    private TMP_Text text = null;
    public string Text 
    {
        get
        {
            if(text == null)
                text = GetComponent<TMP_Text>();
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }


}
