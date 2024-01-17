using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow = null;
    [SerializeField] private UIManager uiManager = null;
    private void Awake()
    {
    }
    private void Start()
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
    private void OnEnable()
    {
    }
}

