using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IButton : MonoBehaviour, IPointerEnterHandler , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.Play("ButtonPress");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.Play("ButtonMouseEnterHandler");
    }
}
