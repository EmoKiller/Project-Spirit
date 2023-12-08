using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GameManager : SerializedMonoBehaviour
{
    public enum Script
    {
        GameManager
    }
    public SpaceState SpaceState;
    [SerializeField] private Player player = null;
    [SerializeField] private CameraFollow cameraFollow = null;
    [SerializeField] private UIManager uiManager = null;
    public float num = 0;
    [SerializeField] BaseStartGame BaseStart = null;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        ConfigDataHelper.GetStartGameDataBase();
    }
    private void Start()
    {
        //BaseStart = ConfigDataHelper.BaseStartGame;
        player.Init();
        cameraFollow.Init();
        uiManager.Init(BaseStart.BaseHP);
    }
    private void ChestBonus()
    {
        ChestBonus obj = Resources.Load<ChestBonus>("Chests/Chest_Wood");
        Instantiate(obj);
    }


}

