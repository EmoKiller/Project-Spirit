using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class IntroGame : MonoBehaviour 
{
    public enum Script
    {
        IntroGame
    }
    [SerializeField] GameObject map1;
    [SerializeField] GameObject map2;
    [SerializeField] GameObject clutLeaders;
    [SerializeField] GameObject bloodImage;
    [SerializeField] GameObject hideWall;
    [SerializeField] List<Enemy> enemy = new List<Enemy>();
    [SerializeField] WayPoint waypoint;
    [SerializeField] Transform ponitDead;
    [SerializeField] GameObject video = null;
    [SerializeField] GameObject ObjuiChoose = null;
    [SerializeField] Transform UImanager = null;
    [SerializeField] Transform TargetCrown = null;
    [SerializeField] DoorController doorNextScren = null;
    Color32 inMap1 = new Color32(0, 0, 0, 255);
    Color32 inMap2 = new Color32(242, 236, 222, 255);
    private void Start()
    {
        EventDispatcher.Addlistener(Script.IntroGame, Events.EnemyGoToWayPoint, EnemyGoToWayPoint);
        EventDispatcher.Addlistener(Script.IntroGame, Events.GoToMap2, GotoMap2);
        EventDispatcher.Addlistener(Script.IntroGame, Events.GoToMap1, GotoMap1);
        EventDispatcher.Addlistener(Script.IntroGame, Events.PlayTalkScript3, TalkScript3);
        EventDispatcher.Addlistener<bool>(Script.IntroGame, Events.SetVideoIntro, SetPlayVideos);
        foreach (Enemy enemy in enemy)
            enemy.SetStay();
        enemy[10].gameObject.SetActive(false);
        enemy[11].gameObject.SetActive(false);
    }
    private void EnemyGoToWayPoint()
    {
        enemy[0].SetMoveWayPoint(waypoint.points[0].position, 2f);
        enemy[0].SetOnEvent(true);
        enemy[1].SetMoveWayPoint(waypoint.points[1].position, 2f);
        enemy[1].SetOnEvent(true);
        for (int i = 4; i <= 8; i++)
            enemy[i].TriggerAni("Pray");
    }
    private void Push()
    {
        enemy[0].Push();
        enemy[1].Push();
    }
    public void EndTalk4()
    {
        
        EventDispatcher.Publish(Player.Script.Player, Events.MoveToWaypoint, ponitDead.position, 1f);
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraTargetPlayer);
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraFocus);
        EventDispatcher.Publish(Player.Script.Player, Events.PlayerTriggerAni, "Detention");
        EventDispatcher.Publish(Bruter.Script.Bruter, Events.BruterTriggerAni);
        EventDispatcher.Publish(Player.Script.Player, Events.SetOnEvent, true);
        this.DelayCall(9.9f, () =>
        {
            EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraDefault);
            EventDispatcher.Publish(Player.Script.Player, Events.SetOnEvent, false);
        });
    }

    public void GotoMap2()
    {
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraTargetPlayer);
        clutLeaders.SetActive(false);
        hideWall.SetActive(false);
        map1.SetActive(false);
        map2.SetActive(true);
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeColorBackGround, inMap2);
    }
    public void OpenUIchoose()
    {
        Instantiate(ObjuiChoose, UImanager);
    }
    public void TalkScript3()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UIButtonReset);
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, TargetCrown);
        EventDispatcher.Publish(Player.Script.Player, Events.SetOnEvent, true);
        EventDispatcher.Publish(Player.Script.Player, Events.MoveToWaypoint, ponitDead.position, 8f);
        EventDispatcher.Publish(WhoWait.Script.WhoWait, Events.WhoWaitTriggerAni);
    }
    public void GotoMap1()
    {
        map1.SetActive(true);
        map2.SetActive(false);
        bloodImage.SetActive(true);
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraTargetPlayer);
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeColorBackGround, inMap1);
        
    }
    private void SetPlayVideos(bool value)
    {
        video.SetActive(value);
        VideoPlayer videoPlayer = video.GetComponent<VideoPlayer>();
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraFocus);
        enemy[10].gameObject.SetActive(true);
        enemy[11].gameObject.SetActive(true);
        this.DelayCall((float)videoPlayer.length, () =>
        {
            video.SetActive(false);
            EventDispatcher.Publish(Player.Script.Player, Events.PlayerTriggerAni, "TakeWeapon");
            EventDispatcher.Publish(Player.Script.Player, Events.SetWeapon);
            for (int i = 4; i <= 8; i++)
                enemy[i].TriggerAni("PrayFear");
            this.DelayCall(1.5f, () =>
            {
                doorNextScren.SetDoor(false);
                enemy[0].SetOnEvent(false);
                enemy[1].SetOnEvent(false);
                foreach (Enemy enemy in enemy)
                    enemy.SetAction(false);
                for (int i = 4; i <= 8; i++)
                {
                    enemy[i].TriggerAni("PrayFear");
                    enemy[i].SetStay();
                }
                EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraDefault);
                EventDispatcher.Publish(Player.Script.Player, Events.SetOnEvent, false);
            });
        });
    }

}
