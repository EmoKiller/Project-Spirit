using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    public enum Script
    {
        GameManager
    }

    [SerializeField] private CameraFollow cameraFollow = null;
    [SerializeField] private UIManager uiManager = null;
    public float num = 0;
    private void Awake()
    {
        
        
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        InfomationPlayerManager.Instance.Init();
        Player.Instance.Init();
        cameraFollow.Init();
        if (InfomationPlayerManager.Instance.PlayerOnScenes() == OnScenes.IntroGame)
        {
            return;
        }
        uiManager.Init();
    }
}

