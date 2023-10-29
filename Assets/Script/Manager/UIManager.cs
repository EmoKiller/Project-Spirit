using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIButtonAction buttonActionE;
    public static Action<string> UpdateTextButton;
    IEnumerator fillIncreaseAction;
    IEnumerator fillReduceAction;

    private void Start()
    {
        buttonActionE.gameObject.SetActive(false);
        
        
    }
    private void OnEnable()
    {
        UpdateTextButton = UpdateText;
        EventDispatcher.AddListener(Events.SetDefaultButton, ResetButton);
        EventDispatcher.AddListener(Events.OnPlayerActionItemsButtonDown, ButtonDown);
        EventDispatcher.AddListener(Events.OnPlayerActionItemsButtonUp, ButtonUp);
    }
    private void OnDisable()
    {
        UpdateTextButton = null;
        EventDispatcher.RemoveListener(Events.SetDefaultButton, ResetButton);
        EventDispatcher.RemoveListener(Events.OnPlayerActionItemsButtonDown, ButtonDown);
        EventDispatcher.RemoveListener(Events.OnPlayerActionItemsButtonUp, ButtonUp);
    }

    private void UpdateText(string str)
    {
        buttonActionE.UpdateText(str);
    }
    private void ResetButton()
    {
        buttonActionE.ResetButton();
    }
    private void ButtonDown()
    {
        fillIncreaseAction = FillIncreaseAction(buttonActionE.FillValue());
        if (fillReduceAction != null)
            StopCoroutine(fillReduceAction);
        StartCoroutine(fillIncreaseAction);

    }
    private void ButtonUp()
    {
        StopCoroutine(fillIncreaseAction);
        fillReduceAction = FillReduceAction(buttonActionE.FillValue());
        StartCoroutine(fillReduceAction);
        
        
    }
    IEnumerator FillIncreaseAction(float amount)
    {
        while (amount <= 1)
        {
            amount += 0.4f * Time.deltaTime;
            buttonActionE.FillUpdate(amount);
            yield return null;
        }
    }
    IEnumerator FillReduceAction(float amount)
    {
        while (amount > 0)
        {
            amount -= 0.4f * Time.deltaTime;
            buttonActionE.FillUpdate(amount);
            Debug.Log(amount);
            yield return null;
        }
    }
}
