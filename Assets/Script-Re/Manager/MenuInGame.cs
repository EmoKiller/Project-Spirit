using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using UnityEditor;
using UnityEngine.Events;

public class MenuInGame : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<MenuType, Button> mainMenu;
    [SerializeField] GameObject objOnQuit;
    [SerializeField] GameObject objPopup;
    public UnityAction OnResume = null;
    private void Awake()
    {
        mainMenu[MenuType.Settings].onClick.AddListener(Settings);
        mainMenu[MenuType.Quit].onClick.AddListener(Quit);
        mainMenu[MenuType.Resume].onClick.AddListener(Resume);
        mainMenu[MenuType.Help].onClick.AddListener(Help);
        mainMenu[MenuType.MainMenu].onClick.AddListener(MainMenu);
        mainMenu[MenuType.BackToMenu].onClick.AddListener(BackToMenu);
        mainMenu[MenuType.QuitGame].onClick.AddListener(QuitGame);
    }
    private void Settings()
    {
        throw new NotImplementedException();
    }

    private void Quit()
    {
        objOnQuit.gameObject.SetActive(true);
        objPopup.gameObject.SetActive(true);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void BackToMenu()
    {
        objOnQuit.gameObject.SetActive(false);
        objPopup.gameObject.SetActive(false);
    }
    private void Resume()
    {
        OnResume?.Invoke();
    }

    private void Help()
    {
        throw new NotImplementedException();
    }

    private void MainMenu()
    {
        AudioManager.instance.StopListSound();
        Time.timeScale = 1f;
        LoadSceneExtension.LoadScene("MainMenu");
    }
}
