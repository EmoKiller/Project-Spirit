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
        InfomationPlayerManager.Instance.Init();
        player.Init();
        cameraFollow.Init();
        uiManager.Init();
        GameLevelManager.Instance.Init();
    }
}

