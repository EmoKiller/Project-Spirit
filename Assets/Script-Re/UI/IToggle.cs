using System;
using UnityEngine;
using UnityEngine.UI;

public class IToggle : MonoBehaviour
{
    public TypeMenuTab TypeMenu;
    protected Image image = null;
    protected Toggle _toggle = null;
    public Toggle Toggle 
    {
        get => this.TryGetMonoComponent(ref _toggle);
    }
    public Action<IToggle,bool> OnChangedEvent = null;
    private void Awake()
    {
        image = GetComponent<Image>();
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }
    protected void OnValueChanged(bool value)
    {
        OnChangedEvent?.Invoke(this, value);
        if (Toggle.isOn == true)
        {
            image.sprite = Toggle.spriteState.selectedSprite;
            return;
        }
        image.sprite = Toggle.spriteState.disabledSprite;
    }
}
