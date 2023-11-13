using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpTalk : MonoBehaviour
{
    public PopUpTalkObject popup = null;
    public List<Transform> pointTalk = null;
    public UnityEvent events;
    int Count = 0;
    private void Start()
    {
        EventDispatcher.Addlistener(ListScript.PopUpTalk, Events.AddListener, Addlistener);
    }
    private void Addlistener()
    {
        EventDispatcher.Addlistener(ListScript.PopUpTalk, Events.OpenPopup, StartScript);
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.AddListener);
    }
    private void StartScript()
    {
        if (Count >= popup.ListText.Count)
        {
            events?.Invoke();
            EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.ClosePopup);
            EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.UiSelect, false);
            return;
        }
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.Open);
        EventDispatcher.Publish(ListScript.CameraFollow,Events.SetSmooth,0.6f);
        EventDispatcher.Publish(ListScript.CameraFollow,Events.UpdateTransform, pointTalk[Count]);
        EventDispatcher.Publish(ListScript.PopUpTalkManager,Events.OpenPopup, popup.ListText[Count]);
        Count++;
    }
}