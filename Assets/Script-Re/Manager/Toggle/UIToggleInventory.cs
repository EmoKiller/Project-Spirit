using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIToggleInventory : UIToggle
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        TogglePanel<UIToggleInventory>.ToggleAction?.Invoke(this);
    }
}
