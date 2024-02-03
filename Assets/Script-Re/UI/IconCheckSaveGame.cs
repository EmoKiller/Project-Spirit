using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class IconCheckSaveGame : MonoBehaviour
{
    public SaveGameSlot Slot;
    public StateSaveGame StateSave;
    [SerializeField] TMP_Text newGame;
    private void Start()
    {
        Initialized();
    }
    public void Initialized()
    {
        Debug.Log(ConfigDataHelper.HeroData.PlayerOnSceness[Slot]);
        if (ConfigDataHelper.HeroData.PlayerOnSceness.ContainsKey(Slot))
        {
            if (ConfigDataHelper.HeroData.PlayerOnSceness[Slot] == OnScenes.IntroGame)
            {
                StateSave = StateSaveGame.NewGame;
                newGame.text = StateSave.ToString();
                return;
            }
            StateSave = StateSaveGame.Continue;
            newGame.text = StateSave.ToString();
        }
        Debug.Log("SavegameLoad" + " " + ConfigDataHelper.HeroData.PlayerOnSceness[Slot]);

    }
}
