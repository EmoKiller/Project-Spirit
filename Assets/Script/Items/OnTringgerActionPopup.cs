using UnityEngine;

public class OnTringgerActionPopup : MonoBehaviour
{
    public PopUpTalkObject popup = null;
    [SerializeField] Transform pointTalk;
    int Count = 0;
    protected virtual void OnTriggerEnter(Collider other)
    {
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.AddListener);
        StartScript();
        EventDispatcher.Addlistener(ListScript.PopUpTalk, Events.OpenPopup, StartScript);
        EventDispatcher.Addlistener(ListScript.OnTringgerAction, Events.OpenPopup, TringgerAction);
    }
    protected virtual void OnTriggerExit(Collider other)
    {

    }
    protected virtual void TringgerAction()
    {
        EventDispatcher.Publish(ListScript.PopUpTalk, Events.AddListener);
        EventDispatcher.Publish(ListScript.PopUpTalk, Events.OpenPopup);
    }
    private void StartScript()
    {
        if (Count >= popup.ListText.Count)
        {
            EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.UiSelect,true);
            return;
        }
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.Open);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.SetSmooth, 0.6f);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, pointTalk);
        EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.OpenPopup, popup.ListText[Count]);
        Count++;
    }
}
