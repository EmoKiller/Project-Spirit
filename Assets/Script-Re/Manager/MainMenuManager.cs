using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenuManager : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<MenuType, Button> mainMenu ;
    [SerializeField] Dictionary<MenuType,List<GameObject>> mainMenuObj;
    [SerializeField] Dictionary<SaveGameSlot, IconCheckSaveGame> saveGame;
    [SerializeField] private Camera cameraMenu;

    [SerializeField] private GameObject waterway;
    [SerializeField] private Image bloomBloom;
    [SerializeField] bool PressStart = false;
    private void Awake()
    {
        
    }
    void Start()
    {
        mainMenuObj[MenuType.PressToPlay][1].gameObject.SetActive(false);
        //mainMenu[MenuType.PressToPlay].onClick.AddListener(PressToPlay);
        mainMenu[MenuType.Play].onClick.AddListener(Play);
        mainMenu[MenuType.Settings].onClick.AddListener(Setting);
        mainMenu[MenuType.Credits].onClick.AddListener(Credits);
        mainMenu[MenuType.RoadMap].onClick.AddListener(RoadMap);
        mainMenu[MenuType.Quit].onClick.AddListener(Quit);
        mainMenu[MenuType.buttonBack].onClick.AddListener(BackToMenu);
        mainMenu[MenuType.BackToMenu].onClick.AddListener(BackToMenu);
        mainMenu[MenuType.backOnStartMenu].onClick.AddListener(BackToMenu);
        mainMenu[MenuType.SaveGame1].onClick.AddListener(SaveGame1);
        mainMenu[MenuType.SaveGame2].onClick.AddListener(SaveGame2);
        mainMenu[MenuType.SaveGame3].onClick.AddListener(SaveGame3);
    }
    private void Update()
    {
        if (PressStart)
            return;
        if (Input.anyKeyDown)
        {
            PressStart = true;
            PressToPlay();
        }

    }
    private void SaveGame1()
    {
        CheckSaveGame(SaveGameSlot.Slot1);
    }
    private void SaveGame2()
    {
        CheckSaveGame(SaveGameSlot.Slot2);
    }
    private void SaveGame3()
    {
        CheckSaveGame(SaveGameSlot.Slot3);
    }
    private void CheckSaveGame(SaveGameSlot slot)
    {
        InfomationPlayerManager.Instance.SaveSlot = slot;
        LoadSceneExtension.LoadScene(ConfigDataHelper.HeroData.PlayerOnSceness[slot].ToString());
    }


    private void SetActiveObj(MenuType type , bool value)
    {
        foreach (var item in mainMenuObj[type])
        {
            item.gameObject.SetActive(value);
        }
    }

    private void PressToPlay()
    {
        SetActiveObj(MenuType.PressToPlay,false);
        cameraMenu.transform.DOLocalMoveZ(-20, 3, false).OnComplete(() =>
        {
            cameraMenu.transform.DOLocalMoveZ(-25, 1, false).OnComplete(() =>
            {
                mainMenuObj[MenuType.PressToPlay][1].gameObject.SetActive(true);
            });
        });
    }
    private void Play()
    {
        mainMenuObj[MenuType.Play][0].gameObject.SetActive(false);
        mainMenuObj[MenuType.Play][1].gameObject.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Setting()
    {
        WaterWayMove();
        SetActiveObj(MenuType.Settings, true);
    }
    private void Credits()
    {
        WaterWayMove();
        SetActiveObj(MenuType.Credits, true);
    }
    private void RoadMap()
    {
        bloomBloom.color = new Color32(0, 0, 0, 225);
        SetActiveObj(MenuType.RoadMap, true);
    }
    private void ExitGame()
    {
        Debug.Log("Exit Game");
    }
    private void Quit()
    {
        bloomBloom.color = new Color32(255, 0, 0, 225);
        SetActiveObj(MenuType.Quit, true);
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
        SetActiveObj(MenuType.BackToMenu, false);
        WaterWayBack();
        mainMenuObj[MenuType.BackToMenu][7].gameObject.SetActive(true);
    }


}
