using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button buttonPressToPlay;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSetting;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonRoadmap;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonAccept;
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonReset;
    [SerializeField] private Button buttonOnQuitBack;
    [SerializeField] private Button buttonOnQuitAccept;
    [SerializeField] private Button backOnStartMenu;
    [SerializeField] private Camera cameraMenu;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject save;
    [SerializeField] private GameObject grMenu;
    [SerializeField] private GameObject presstost;
    [SerializeField] private GameObject waterway;
    [SerializeField] private GameObject onStart;
    [SerializeField] private GameObject onSetting;
    [SerializeField] private GameObject onCredits;
    [SerializeField] private GameObject onRoadMap;
    [SerializeField] private GameObject onQuit;
    [SerializeField] private GameObject accept;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject reset;
    [SerializeField] private GameObject bloomBgr;

    void Start()
    {
        buttonPressToPlay.onClick.AddListener(PressToPlay);
        buttonPlay.onClick.AddListener(Play);
        buttonSetting.onClick.AddListener(Setting);
        buttonCredits.onClick.AddListener(Credits);
        buttonRoadmap.onClick.AddListener(RoadMap);
        buttonQuit.onClick.AddListener(Quit);
        buttonBack.onClick.AddListener(BackToMenu);
        buttonOnQuitAccept.onClick.AddListener(ExitGame);
        buttonOnQuitBack.onClick.AddListener(BackToMenu);
        backOnStartMenu.onClick.AddListener(BackToMenu);
    }
    private void Update()
    {

    }
    private void PressToPlay()
    {
        presstost.gameObject.SetActive(false);
        cameraMenu.transform.DOLocalMoveZ(-20, 3, false).OnComplete(() =>
        {
            cameraMenu.transform.DOLocalMoveZ(-25, 1, false).OnComplete(() =>
            {
                grMenu.gameObject.SetActive(true);
            });
        });
    }
    private void Play()
    {
        menu.gameObject.SetActive(false);
        save.gameObject.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Setting()
    {
        WaterWayMove();
        accept.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        reset.gameObject.SetActive(true);
    }
    private void Credits()
    {
        WaterWayMove();
        onCredits.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
    }
    private void RoadMap()
    {
        bloomBgr.gameObject.SetActive(true);
        Image img = bloomBgr.GetComponent<Image>();
        img.color = new Color32(0,0,0,225);
        onRoadMap.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
    }
    private void ExitGame()
    {
        Debug.Log("Exit Game");
    }
    private void Quit()
    {
        bloomBgr.gameObject.SetActive(true);
        onQuit.gameObject.SetActive(true);
        Image img = bloomBgr.GetComponent<Image>();
        img.color = new Color32(255, 0, 0, 225);
        accept.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
    }
    private void WaterWayMove()
    {
        waterway.transform.DOLocalMoveX(100, 1, false);
    }
    private void WaterWayBack()
    {
        waterway.transform.DOLocalMoveX(-1980, 1, false);
    }
    private void BackToMenu()
    {
        bloomBgr.gameObject.SetActive(false);
        onQuit.gameObject.SetActive(false);
        WaterWayBack();
        onRoadMap.gameObject.SetActive(false);
        accept.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        reset.gameObject.SetActive(false);
        save.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        
    }


}
