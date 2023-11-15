using System;
using UnityEngine;

public class TogglePanel<T> : MonoBehaviour where T : UIToggle
{
    public static Action<T> ToggleAction;
    [SerializeField] private T current = null;
    [SerializeField] protected Sprite img;
    [SerializeField] protected Sprite imgNull;
    [SerializeField] protected Color color;
    [SerializeField] protected Color colorNull;

    private void Start()
    {
        ToggleAction = onHanger;
        current.uiImg.sprite = img;
    }
    public virtual void onHanger(T Tog)
    {
        if (Tog != current)
        {
            Tog.uiImg.sprite = img;
            Tog.text.color = color;
            current.uiImg.sprite = imgNull;
            current.text.color = colorNull;
            current = Tog;
        }
    }
    private void OnDestroy()
    {
        ToggleAction = null;
    }
}
