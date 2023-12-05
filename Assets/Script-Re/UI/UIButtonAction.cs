using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAction : MonoBehaviour
{
    public enum Script
    {
        UIButtonAction
    }
    [SerializeField] private GameObject buttonE;
    [SerializeField] private GameObject mouseClick;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image fill;
    [SerializeField] private RectTransform rectButton => GetComponent<RectTransform>();
    [SerializeField] private RectTransform rectShowText;
    [SerializeField] private RectTransform thisTransform;
    Coroutine fillIncreaseAction;
    Coroutine fillReduceAction;
    private void Start()
    { 
        gameObject.SetActive(false);
        buttonE.SetActive(false);
        mouseClick.SetActive(false);
        EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIButtonAction, Events.UIButtonOpen, UIButtonOpen);
        EventDispatcher.Addlistener(Script.UIButtonAction, Events.UIButtonReset, ResetButton);
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
    private void OnEnable()
    {
        
    }
    private void UIButtonOpen(TypeShowButton type,string str)
    {
        switch (type)
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
        gameObject.SetActive(true);
        rectButton.sizeDelta += new Vector2(100,0);
        rectButton.sizeDelta += new Vector2((str.Length * 25), 0);
        text.text = str;
        LayoutRebuilder.ForceRebuildLayoutImmediate(thisTransform);
    }
    private float FillValue()
    {
        return fill.fillAmount;
    }
    private void FillUpdate(float amount)
    {
        fill.fillAmount = amount;
    }
    private void ResetButton()  
    {
        gameObject.SetActive(false);
        rectButton.sizeDelta = new Vector2(0, 110);
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


