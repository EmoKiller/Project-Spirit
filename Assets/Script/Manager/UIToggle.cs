using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuTypezzz type;
    public Action<MenuTypezzz> OnSelected = null;
    [SerializeField] Sprite _sprite;
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    private Toggle toggle = null;


    private void Awake()
    {
        image = GetComponent<Image>();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.white;
        ToggleOn();
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    private void OnValueChanged(bool isOn)
    {
        image = GetComponent<Image>();
        
        image.sprite = _sprite;
        if (isOn)
        {
            OnSelected?.Invoke(type);
        }
            
        

        //if (iconFocus != null)
        //    iconFocus.SetActive(isOn);

        //if (tabFocus != null)
        //    tabFocus.SetActive(isOn);

        //if (isOn)
        //    OnSelected?.Invoke(type);
    }
    public void ToggleOn() => toggle.isOn = true;
    public void ToggleOff() => toggle.isOn = false;

    
}

