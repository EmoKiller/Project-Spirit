using System;
using UnityEngine;

public class ToggleManagerOnQuit : MonoBehaviour
{
    public static Action<UIToggelOnQuit> ToggleAction2;
    [SerializeField] private UIToggelOnQuit current = null;
    [SerializeField] protected Sprite img;
    [SerializeField] protected Sprite imgNull;
    private void Awake()
    {
        ToggleAction2 = onHangerr;
        current.uiImg.sprite = img;
        current.text.color = Color.white;
    }
    public void onHangerr(UIToggelOnQuit Tog)
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
        ToggleAction2 = null;
    }
}
