using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UIManager : MonoBehaviour
{
    public static Action<string> UpdateStringButtonE;
    [SerializeField] protected TMP_Text text;
    
    private void OnEnable()
    {
        UpdateStringButtonE = OnTriggerItems;
    }
    private void OnDisable()
    {
        UpdateStringButtonE = null;
    }
    private void OnTriggerItems(string str)
    {
        Debug.Log(str.Length);
        text.text = str;
    }
}
