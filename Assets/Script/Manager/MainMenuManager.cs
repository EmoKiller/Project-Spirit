using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSetting;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonQuit;

    void Start()
    {
        buttonPlay.onClick.AddListener(Play);
        buttonSetting.onClick.AddListener(Setting);
        buttonCredits.onClick.AddListener(Credits);
        buttonQuit.onClick.AddListener(Quit);
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
