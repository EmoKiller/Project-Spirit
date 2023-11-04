using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpTalk : MonoBehaviour
{
    [SerializeField] UnityEvent events;
    [SerializeField] List<string> list;
    [SerializeField] Transform[] waypoints; 
    [SerializeField] private int count = 0;

    private void Start()
    {
        EventDispatcher.Addlistener(ListScript.PopUpTalk, Events.AddListener, AddListenerBoxTalk);
    }

    private void AddListenerBoxTalk()
    {
        EventDispatcher.Addlistener(ListScript.PopUpTalk, Events.OpenTalkBox, TalkingText);
    }
    private void TalkingText()
    {
        //if (count >= list.Count)
        //{
        //    events?.Invoke();
        //    return;
        //}
        //EventDispatcher.Publish(ListScript.CameraFollow, Events.SetSmooth, 0.6f);
        //EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, waypoints.points[count]);
        //EventDispatcher.Publish(ListScript.TalkTime, Events.OpenBoxTalk, list[count]);
        //count++;
    }
}
