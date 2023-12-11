using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
public class UIButtonAction : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<TypeUIButton, UITypeButton> m_TypeButton = new Dictionary<TypeUIButton, UITypeButton>();
    public Dictionary<TypeUIButton, UITypeButton> TypeButton
    {
        get
        {
            if (m_TypeButton.Count == 0)
            {
                List<UITypeButton> type = GetComponentsInChildren<UITypeButton>().ToList();
                m_TypeButton = new Dictionary<TypeUIButton, UITypeButton>();
                foreach (var item in type)
                {
                    m_TypeButton.Add(item.Type, item);
                }
            }
            return m_TypeButton;
        }
    }
    public RectTransform rectButton => GetComponent<RectTransform>();
    public Coroutine fillIncreaseAction;
    public Coroutine fillReduceAction;
    public Action OnButtonDown = null;
    public Action OnButtonUp = null;
    public Action OnTriggerUpdateFillValue = null;
    private void Awake()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnButtonDown?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            OnButtonUp?.Invoke();
        }
        if (FillValue() >= 1)
        {
            OnTriggerUpdateFillValue?.Invoke();
        }
    }
    public float FillValue()
    {
        return TypeButton[TypeUIButton.ButtonE].FillAmount;
    }
    public void FillUpdate(float amount)
    {
        TypeButton[TypeUIButton.ButtonE].FillAmount = amount;
    }
    public void UpdateText(string str)
    {
        TypeButton[TypeUIButton.TextShow].Text = str;
    }
}


