using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static Action<string> UpdateStringButtonE;
    [SerializeField] private RectTransform buttonActionE;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image fill;
    public static Action<float> imageEvent;

    private void Awake()
    {
        buttonActionE.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        imageEvent = FillAmount;
        UpdateStringButtonE = OnTriggerItems;
        EventDispatcher.AddListener(Events.OnTriggerItems, SetOffButtonE);
    }
    private void OnDisable()
    {
        UpdateStringButtonE = null;
        imageEvent = null;
        EventDispatcher.RemoveListener(Events.OnTriggerItems, SetOffButtonE);
    }
    private void FillAmount(float amount)
    {
        fill.fillAmount += amount;
    }
    private void OnTriggerItems(string str)
    {
        buttonActionE.gameObject.SetActive(true);
        buttonActionE.sizeDelta = new Vector2(buttonActionE.sizeDelta.x + (str.Length*24), 110);
        text.text = str;
    }
    private void SetOffButtonE()
    {
        buttonActionE.gameObject.SetActive(false);
        buttonActionE.sizeDelta = new Vector2(125, 110);
    }
}
