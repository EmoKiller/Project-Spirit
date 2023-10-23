using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] protected TMP_Text text;
    
    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(this);
    }
    private void OnEnable()
    {
        EventDispatcher.AddListener(Events.OnTriggerItems, OnTriggerItems);
    }
    private void OnDisable()
    {
        Instance = null;
        EventDispatcher.RemoveListener(Events.OnTriggerItems, OnTriggerItems);
    }
    private void OnTriggerItems()
    {

    }
}
