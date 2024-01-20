using System;
using UnityEngine;
using UnityEngine.UI;

public class IToggle : MonoBehaviour
{
    public TypeMenuTab TypeMenu;
    [SerializeField] protected Image image = null;
    [SerializeField] protected Toggle _toggle = null;
    public Toggle Toggle { get => this.TryGetMonoComponent(ref _toggle); }
    public Action<IToggle, bool> OnChangedEvent;
    private void Awake()
    {
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }
    protected virtual void OnValueChanged(bool value)
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
