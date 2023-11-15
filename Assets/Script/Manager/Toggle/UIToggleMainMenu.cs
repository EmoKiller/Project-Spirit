using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIToggleMainMenu : UIToggle
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        TogglePanel<UIToggleMainMenu>.ToggleAction?.Invoke(this);
    }
}
