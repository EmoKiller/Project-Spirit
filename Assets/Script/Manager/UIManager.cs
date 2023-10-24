using System;
using TMPro;
using UnityEngine;
 
public class UIManager : MonoBehaviour
{
    public static Action<string> UpdateStringButtonE;
    public RectTransform buttonActionE;
    [SerializeField] protected TMP_Text text;
    
    private void OnEnable()
    {
        UpdateStringButtonE = OnTriggerItems;
        EventDispatcher.AddListener(Events.OnTriggerItems, SetOffButtonE);
    }
    private void OnDisable()
    {
        UpdateStringButtonE = null;
        EventDispatcher.RemoveListener(Events.OnTriggerItems, SetOffButtonE);
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
