using System;
using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    public static Action<UIToggle> ToggleAction;
    [SerializeField] private UIToggle current = null;
    [SerializeField] protected Sprite img;
    [SerializeField] protected Sprite imgNull;
    private void Awake()
    {
        ToggleAction = onHanger;
        current.uiImg.sprite = img;
        current.text.color = Color.white;
    }
    public virtual void onHanger(UIToggle Tog)
    {
        if (Tog != current)
        {
            Tog.uiImg.sprite = img;
            Tog.text.color = Color.white;
            current.uiImg.sprite = imgNull;
            current.text.color = new Color32(20, 73, 45, 255);
            current = Tog;
        }
        
    }
    private void OnDestroy()
    {
        ToggleAction = null;
    }
}
