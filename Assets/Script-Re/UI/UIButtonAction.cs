using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAction : MonoBehaviour
{
    public enum Script
    {
        UIButtonAction
    }
    private Dictionary<TypeUIButton, UITypeButton> m_TypeButton = new Dictionary<TypeUIButton, UITypeButton>();
    public Dictionary<TypeUIButton, UITypeButton> TypeButton
    {
        get
        {
            if (m_TypeButton.Count == 0)
            {
                List<UITypeButton> type = GetComponentsInChildren<UITypeButton>().ToList();
                foreach (var item in type)
                {
                    m_TypeButton.Add(item.Type, item);
                }
            }
            return m_TypeButton;
        }
    }
    [SerializeField] private TMP_Text text;
    [SerializeField] public RectTransform rectButton => GetComponent<RectTransform>();
    [SerializeField] private RectTransform rectShowText;
    Coroutine fillIncreaseAction;
    Coroutine fillReduceAction;

    private void Start()
    {
        
        //gameObject.SetActive(false);
        TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
        //EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIButtonAction, Events.UIButtonOpen, UIButtonOpen);
        //EventDispatcher.Addlistener(Script.UIButtonAction, Events.UIButtonReset, ResetButton);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ButtonDown();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            ButtonUp();
        }
        if (FillValue() >= 1)
        {
            EventDispatcher.Publish(TriggerWaitAction.Script.TriggerWaitAction, Events.OnTringgerWaitAction);
        }
    }
    public void SwitchTypeButton(TypeShowButton type)
    {
        switch (type)
        {
            case TypeShowButton.Talk:
            case TypeShowButton.TakeWeapon:
                TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
                break;
            case TypeShowButton.Items:
                TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
                break;
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
        text.text = str;
    }
    
    private void ResetButton()  
    {
        gameObject.SetActive(false);
        rectButton.sizeDelta = new Vector2(0, 110);
        rectShowText.sizeDelta = rectButton.sizeDelta;
        TypeButton[TypeUIButton.ButtonE].FillAmount = 0f;
        TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
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
            FillUpdate(amount);
            yield return null;
        }
    }
    IEnumerator FillReduceAction(float amount)
    {
        while (amount > 0)
        {
            amount -= 1f * Time.deltaTime;
            FillUpdate(amount);
            yield return null;
        }
    }
}


