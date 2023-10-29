using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAction : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image fill;
    [SerializeField] private RectTransform rectButton => GetComponent<RectTransform>();
    [SerializeField] private RectTransform rectShowText;
    public float FillValue()
    {
        return fill.fillAmount;
    }
    public void FillUpdate(float amount)
    {
        fill.fillAmount = amount;
    }
    public void ResetButton()
    {
        gameObject.SetActive(false);
        rectButton.sizeDelta = new Vector2(125, 110);
        rectShowText.sizeDelta = rectButton.sizeDelta;
    }
    public void UpdateText(string str)
    {
        gameObject.SetActive(true);
        rectButton.sizeDelta = new Vector2(rectButton.sizeDelta.x + (str.Length * 25), 110);
        rectShowText.sizeDelta = rectButton.sizeDelta;
        text.text = str;
    }
    
}
