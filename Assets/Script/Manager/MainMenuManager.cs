using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button buttonPressToPlay;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSetting;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Camera cameraMenu;
    [SerializeField] private Image logo;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject presstost;

    void Start()
    {
        buttonPressToPlay.onClick.AddListener(PressToPlay);
        //buttonPlay.onClick.AddListener(Play);
        //buttonSetting.onClick.AddListener(Setting);
        //buttonCredits.onClick.AddListener(Credits);
        //buttonQuit.onClick.AddListener(Quit);
        
    }
    private void PressToPlay()
    {
        presstost.gameObject.SetActive(false);
        cameraMenu.transform.DOLocalMoveZ(-20, 3, false).OnComplete(() =>
        {
            cameraMenu.transform.DOLocalMoveZ(-25, 1, false).OnComplete(() =>
            {
                menu.gameObject.SetActive(true);
                logo.gameObject.SetActive(true);
            });
        });
    }
    private void Play()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Setting()
    {
        Debug.Log("Setting Game");
    }
    private void Credits()
    {
        Debug.Log("Credits Game");
    }
    private void Quit()
    {
        Debug.Log("Quit Game");
    }


}
