using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour, IPointerEnterHandler
{
    public TMP_Text text;
    public Image uiImg;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        TogglePanel<UIToggle>.ToggleAction?.Invoke(this);
    }
    

    
}

