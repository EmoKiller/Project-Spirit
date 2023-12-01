using UnityEngine.EventSystems;

public class UIToggleOnQuit : UIToggle
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        TogglePanel<UIToggleOnQuit>.ToggleAction?.Invoke(this);
    }
}
