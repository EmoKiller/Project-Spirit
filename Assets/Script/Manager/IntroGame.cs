using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;

public class IntroGame : MonoBehaviour
{
    [SerializeField] GameObject map1;
    [SerializeField] GameObject map2;
    [SerializeField] CameraChangeLookAt objChangeTarget;
    [SerializeField] GameObject hideWall;
    [SerializeField] List<Enemy> enemy = new List<Enemy>();
    [SerializeField] WayPoint waypoint;
    [SerializeField] WayPoint pointInTalk;
    [SerializeField] List<string> OutTalk;
    [SerializeField] private int count = 0;

    Color32 inMap1 = new Color32(0, 0, 0, 255);
    Color32 inMap2 = new Color32(242, 236, 222, 255);

    private void Start()
    {
        EventDispatcher.Addlistener(ListScript.Bruter, Events.TriggerAction2, GotoMap2);
        EventDispatcher.Addlistener(ListScript.IntroGame, Events.AddListener, AddListenerBoxTalk);
        foreach (Enemy enemy in enemy)
            enemy.SetStay();
    }
    private void AddListenerBoxTalk()
    {
        EventDispatcher.Addlistener(ListScript.IntroGame, Events.OpenBoxTalk, TalkingText);
    }
    private void SetWalk()
    {
        enemy[0].SetMoveWayPoint(waypoint.points[0]);
        enemy[1].SetMoveWayPoint(waypoint.points[1]);
    }
    private void TalkingText()
    {
        if (count >= 4)
        {
            EndTalk();
            return;
        }
        EventDispatcher.Publish(ListScript.CameraFollow, Events.SetSmooth,0.8f);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, pointInTalk.points[count]);
        EventDispatcher.Publish(ListScript.TalkTime, Events.OpenBoxTalk, OutTalk[count]);
        count++;
        
    }
    private void EndTalk()
    {
        objChangeTarget.ReturnTransPlayer();
        EventDispatcher.Publish(ListScript.TalkTime, Events.Close);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.CameraZoom);
        EventDispatcher.Publish(ListScript.Bruter, Events.TriggerAction);
    }
    private void Push()
    {
        enemy[0].Push();
        enemy[1].Push();
    }
    private void GotoMap2()
    {
        objChangeTarget.ReturnTransPlayer();
        objChangeTarget.gameObject.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(true);
        
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateColor, inMap2);
    }
    private void GotoMap1()
    {
        map1.SetActive(true);
        map2.SetActive(false);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateColor, inMap1);
    }
}
