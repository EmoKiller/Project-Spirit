using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIToggelOnQuit : MonoBehaviour , IPointerEnterHandler
{
    public TMP_Text text;
    public Image uiImg;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ToggleManager.ToggleAction2?.Invoke(this);
        ToggleManagerOnQuit.ToggleAction2?.Invoke(this);
    }
}
