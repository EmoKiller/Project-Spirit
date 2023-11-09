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
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventDispatcher.Publish(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonDown);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            EventDispatcher.Publish(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonUp);
        }
        if (Input.GetKey(KeyCode.E))
        {
            EventDispatcher.Publish(ListScript.OnTringgerWaitAction, Events.OnTringgerActionItems);
        }
    }
    private void OnEnable()
    {
        EventDispatcher.Addlistener<TypeShowButton>(ListScript.UIButtonAction, Events.SwitchImageButton, SwitchImageButton);
        EventDispatcher.Addlistener<string>(ListScript.UIButtonAction, Events.UpdateText, UpdateText);
        EventDispatcher.Addlistener(ListScript.UIButtonAction, Events.SetDefaultButton, ResetButton);
        EventDispatcher.Addlistener(ListScript.UIButtonAction, Events.AddListener, AddListener);
        EventDispatcher.Addlistener(ListScript.UIButtonAction, Events.RemoveEvent, RemoveEvent);
    }
    private void AddListener()
    {
        EventDispatcher.Addlistener(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonDown, ButtonDown);
        EventDispatcher.Addlistener(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonUp, ButtonUp);
    }
    private void RemoveEvent()
    {
        EventDispatcher.RemoveEvent(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonDown);
        EventDispatcher.RemoveEvent(ListScript.UIButtonAction, Events.OnPlayerActionItemsButtonUp);
    }
    private void SwitchImageButton(TypeShowButton value)
    {
        switch (value)
        {
            case TypeShowButton.Talk:
                buttonE.gameObject.SetActive(true);
                break;
            case TypeShowButton.Items:
                mouseClick.gameObject.SetActive(true);
                break;
            case TypeShowButton.TakeWeapon:
                buttonE.gameObject.SetActive(true);
                break;
        }
    }
    private float FillValue()
    {
        return fill.fillAmount;
    }
    private void FillUpdate(float amount)
    {
        fill.fillAmount = amount;
    }
    private void UpdateText(string str)
    {
        gameObject.SetActive(true);
        rectButton.sizeDelta = new Vector2(rectButton.sizeDelta.x + (str.Length * 33), 110);
        rectShowText.sizeDelta = rectButton.sizeDelta;
        text.text = str;
    }
    private void ResetButton()
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
            amount += 1f * Time.deltaTime;
            EventDispatcher.Publish(ListScript.TypeButton, Events.UpdateValue, amount);
            FillUpdate(amount);
            yield return null;
        }
    }
    IEnumerator FillReduceAction(float amount)
    {
        while (amount > 0)
        {
            amount -= 1f * Time.deltaTime;
            EventDispatcher.Publish(ListScript.TypeButton, Events.UpdateValue, amount);
            FillUpdate(amount);
            yield return null;
        }
    }
}


