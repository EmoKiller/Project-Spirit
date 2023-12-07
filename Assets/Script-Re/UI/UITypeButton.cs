using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


}
