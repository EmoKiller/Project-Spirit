//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Video;

//public class IntroGame : MonoBehaviour
//{

//    [SerializeField] GameObject map1;
//    [SerializeField] GameObject map2;
//    [SerializeField] GameObject clutLeaders;
//    [SerializeField] GameObject bloodImage;
//    [SerializeField] CameraChangeLookAt objChangeTarget;
//    [SerializeField] GameObject hideWall;
//    [SerializeField] List<Enemy> enemy = new List<Enemy>();
//    [SerializeField] WayPoint waypoint;
//    [SerializeField] Transform ponitDead;
//    [SerializeField] GameObject video = null;
//    Color32 inMap1 = new Color32(0, 0, 0, 255);
//    Color32 inMap2 = new Color32(242, 236, 222, 255);

//    private void Start()
//    {
//        //EventDispatcher.Addlistener<bool>(ListScript.VideoPlayer, Events.UpdateValue, SetPlayVideos);
//        //EventDispatcher.Addlistener(ListScript.IntroGame, Events.TriggerAction, SetWalk);
//        //EventDispatcher.Addlistener(ListScript.Bruter, Events.TriggerAction2, GotoMap2);
//        //EventDispatcher.Addlistener(ListScript.WhoWait, Events.TriggerAction2, GotoMap1);
//        foreach (Enemy enemy in enemy)
//            enemy.SetStay();
//    }
//    private void SetWalk()
//    {
//        enemy[0].SetMoveWayPoint(waypoint.points[0].position,1.1f);
//        enemy[1].SetMoveWayPoint(waypoint.points[1].position,1.1f);
//        for (int i = 4;i<=8;i++)
//            enemy[i].TriggerAni("Pray");
//    }
//    private void Push()
//    {
//        enemy[0].Push();
//        enemy[1].Push();
//    }
//    private void GotoMap2()
//    {
//        objChangeTarget.ReturnTransPlayer();
//        objChangeTarget.gameObject.SetActive(false);
//        clutLeaders.SetActive(false);
//        hideWall.SetActive(false);

//        map1.SetActive(false);
//        map2.SetActive(true);
        
//        EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.ChangeColorBackGround, inMap2);
//    }
//    private void GotoMap1()
//    {
//        map1.SetActive(true);
//        map2.SetActive(false);
//        bloodImage.SetActive(true);
//        EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.ReturnTargetPlayer);
//        EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.ChangeColorBackGround, inMap1);
//    }
//    public void EndTalk4()
//    {
//        EventDispatcher.Publish(ListScript.Player, Player.Events.MoveToWaypoint, ponitDead.position, 1f);
//        EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.ReturnTargetPlayer);
//        EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.CameraFocus);
//        //EventDispatcher.Publish(ListScript.Player, Events.TriggerAction);
//        //EventDispatcher.Publish(ListScript.Bruter, Events.TriggerAction);
//        this.DelayCall(9.9f, () =>
//        {
//            EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.CameraDefault);
//            //EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.Close);
//            this.DelayCall(1.5f, () =>
//            {
//                //EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.RemoveEvent);
//            });
//        });
//    }
//    public void EndTalkWhitWhoWaits()
//    {
//        EventDispatcher.Publish(ListScript.Player, Player.Events.MoveToWaypoint, ponitDead.position,8f);
//        //EventDispatcher.Publish(ListScript.WhoWait, Events.TriggerAction);
//    }
//    private void SetPlayVideos(bool value)
//    {
//        video.SetActive(value);
//        VideoPlayer videoPlayer = video.GetComponent<VideoPlayer>();
//        EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.CameraFocus);
//        this.DelayCall((float)videoPlayer.length, () =>
//        {
//            video.SetActive(false);
//            EventDispatcher.Publish(ListScript.Player, Player.Events.TriggerAni,"TakeWeapon");
//            for (int i = 4; i <= 8; i++)
//                enemy[i].TriggerAni("PrayFear");

//            this.DelayCall(1.5f, () =>
//            {
//                foreach (Enemy enemy in enemy)
//                    enemy.SetAction(false);
//                for (int i = 4; i <= 8; i++)
//                {
//                    enemy[i].TriggerAni("PrayFear");
//                    enemy[i].SetStay();
//                }
                    
//                EventDispatcher.Publish(ListScript.CameraFollow, CameraFollow.Events.CameraDefault);
//                //EventDispatcher.Publish(ListScript.PopUpTalkManager, Events.Close);

//            });
//        });
//    }

//}
