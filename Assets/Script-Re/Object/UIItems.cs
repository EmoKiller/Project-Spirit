using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItems : MonoBehaviour
{
    private Sprite sprite;
    public Sprite _Sprite
    {
        get
        {
            if (sprite == null)
                sprite = GetComponent<Sprite>();
            return sprite;
        }
        set
        {
            SetSprite(value);
        }
    }
    private TMP_Text quantity = null;
    public TMP_Text Quantity
    {
        get
        {
            if (quantity == null)
                quantity = GetComponentInChildren<TMP_Text>();
            return quantity;
        }
        set
        {
            
        }
    }
    private void SetSprite(Sprite spr)
    {
        _Sprite = spr;
    }
    private void AddQuantity()
    {

    }

}
