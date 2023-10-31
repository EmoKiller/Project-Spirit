using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private UIButtonAction buttonActionE;
    private void Start()
    {
        buttonActionE.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        EventDispatcher.Addlistener<string>(ListScript.UIManager, Events.SwitchImageButton, SwitchImageButton);
        EventDispatcher.Addlistener<string>(ListScript.UIManager, Events.UpdateText, UpdateText);
        EventDispatcher.Addlistener(ListScript.UIManager, Events.SetDefaultButton, ResetButton);
        EventDispatcher.Addlistener(ListScript.UIManager, Events.AddListener, AddListener);
        EventDispatcher.Addlistener(ListScript.UIManager, Events.RemoveEvent, RemoveEvent);
    }
    private void AddListener()
    {
        EventDispatcher.Addlistener(ListScript.UIManager, Events.OnPlayerActionItemsButtonDown, OnButtonDown);
        EventDispatcher.Addlistener(ListScript.UIManager, Events.OnPlayerActionItemsButtonUp, OnButtonUp);
    }
    private void RemoveEvent()
    {
        EventDispatcher.RemoveEvent(ListScript.UIManager, Events.OnPlayerActionItemsButtonDown);
        EventDispatcher.RemoveEvent(ListScript.UIManager, Events.OnPlayerActionItemsButtonUp);
    }
    private void SwitchImageButton(string value)
    {
        buttonActionE.SwitchImageButton(value);
    }
    private void UpdateText(string str)
    {
        buttonActionE.UpdateText(str);
    }
    private void ResetButton()
    {
        buttonActionE.ResetButton();
    }
    private void OnButtonDown()
    {
        buttonActionE.buttonDown?.Invoke();
    }
    private void OnButtonUp()
    {
        buttonActionE.buttonUp?.Invoke();
    }
}
