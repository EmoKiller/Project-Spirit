using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    public enum Script
    {
        GameManager
    }

    [SerializeField] private Player player = null;
    [SerializeField] private CameraFollow cameraFollow = null;
    [SerializeField] private UIManager uiManager = null;
    public float num = 0;
    private void Awake()
    {
        
        
    }
    private void Start()
    {
        BaseStartGame  a = ConfigDataHelper.BaseStartGame;
        player.Init();
        cameraFollow.Init();
        uiManager.Init(a.BaseHP);

    }
    private void ChestBonus()
    {
        ChestBonus obj = Resources.Load<ChestBonus>("Chests/Chest_Wood");
        Instantiate(obj);
    }
    [Button("SpawnExp")]
    private void SpawnExp()
    {
        GameObject obj = Resources.Load<GameObject>("Object/Exp");
        Instantiate(obj);
    }


}

