using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject buttonE;
    [SerializeField] private GameObject mouseClick;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image fill;
    [SerializeField] private RectTransform rectButton => GetComponent<RectTransform>();
    [SerializeField] private RectTransform rectShowText;
    Coroutine fillIncreaseAction;
    Coroutine fillReduceAction;

    public Action buttonDown;
    public Action buttonUp;

    //private Coroutine demoCrt = null;
    private void OnEnable()
    {
        buttonDown = ButtonDown;
        buttonUp = ButtonUp;
    }
    private void OnDisable()
    {
        buttonDown = null;
        buttonUp = null;
    }
    
    public void SwitchImageButton(string value)
    {
        if (value == "Button")
            buttonE.gameObject.SetActive(true);
        else if (value == "Mouse")
            mouseClick.gameObject.SetActive(true);
    }
    public float FillValue()
    {
        return fill.fillAmount;
    }
    public void FillUpdate(float amount)
    {
        fill.fillAmount = amount;
    }
    public void UpdateText(string str)
    {
        gameObject.SetActive(true);
        rectButton.sizeDelta = new Vector2(rectButton.sizeDelta.x + (str.Length * 25), 110);
        rectShowText.sizeDelta = rectButton.sizeDelta;
        text.text = str;
    }
    public void ResetButton()
    {
        gameObject.SetActive(false);
        rectButton.sizeDelta = new Vector2(125, 110);
        rectShowText.sizeDelta = rectButton.sizeDelta;
        fill.fillAmount = 0f;
        buttonE.gameObject.SetActive(false);
        mouseClick.gameObject.SetActive(false);
    }
    private void ButtonDown()
    {
        if (fillReduceAction != null)
            StopCoroutine(fillReduceAction);
        fillIncreaseAction = StartCoroutine(FillIncreaseAction(FillValue()));
    }
    private void ButtonUp()
    {
        if (fillIncreaseAction != null)
            StopCoroutine(fillIncreaseAction);
        fillReduceAction = StartCoroutine(FillReduceAction(FillValue()));
    }
    IEnumerator FillIncreaseAction(float amount)
    {
        while (amount <= 1)
        {
            amount += 0.4f * Time.deltaTime;
            //EventDispatcher.TriggerEvent(Events.FillAmount, amount);
            FillUpdate(amount);
            yield return null;
        }
    }
    IEnumerator FillReduceAction(float amount)
    {
        while (amount > 0)
        {
            amount -= 0.4f * Time.deltaTime;
            FillUpdate(amount);
            yield return null;
        }
    }
}


