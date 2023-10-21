using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour, IPointerEnterHandler
{
    public TMP_Text text;
    public Image uiImg;
    public void OnPointerEnter(PointerEventData eventData)
    {
        ToggleManager.ToggleAction?.Invoke(this);
    }

    
}

