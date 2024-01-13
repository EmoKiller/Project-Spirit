using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class ConfigDataHelper 
{
    private static GameConfig gameConfig = null;
    public static GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
                gameConfig = JsonConvert.DeserializeObject<GameConfig>(Resources.Load<TextAsset>("Data/GameConfig").text);
            return gameConfig;
        }
    }
    private static HeroData heroData = null;
    public static HeroData HeroData
    {
        get
        {
            heroData = GetHeroData();
            ES3.Save("HeroDatas", GetHeroData());

            //if (!ES3.KeyExists("HeroDatas"))
            //{
            //    heroData = GetHeroData();
            //    ES3.Save("HeroDatas", GetHeroData());
            //}
            //else
            //{
            //    heroData = ES3.Load<HeroData>("HeroDatas");
            //}
            return heroData;
        }
        set => ES3.Save("HeroDatas", value);
    }
    private static HeroData GetHeroData()
    {
        HeroData data = new HeroData();
        data.attributes.Add(SaveGameSlot.Slot1, GameConfig.HeroBaseData);
        data.attributes.Add(SaveGameSlot.Slot2, GameConfig.HeroBaseData);
        data.attributes.Add(SaveGameSlot.Slot3, GameConfig.HeroBaseData);
        data.BaseAttributes.Add(SaveGameSlot.Slot1, GameConfig.HeroBaseData);
        data.BaseAttributes.Add(SaveGameSlot.Slot2, GameConfig.HeroBaseData);
        data.BaseAttributes.Add(SaveGameSlot.Slot3, GameConfig.HeroBaseData);
        data.TarrotAddattributes.Add(SaveGameSlot.Slot1, GameConfig.TarrotAddattributes);
        data.TarrotAddattributes.Add(SaveGameSlot.Slot2, GameConfig.TarrotAddattributes);
        data.TarrotAddattributes.Add(SaveGameSlot.Slot3, GameConfig.TarrotAddattributes);
        data.PowerAddattributes.Add(SaveGameSlot.Slot1, GameConfig.PowerAddattributes);
        data.PowerAddattributes.Add(SaveGameSlot.Slot2, GameConfig.PowerAddattributes);
        data.PowerAddattributes.Add(SaveGameSlot.Slot3, GameConfig.PowerAddattributes);
        data.PlayerOnSceness.Add(SaveGameSlot.Slot1, OnScenes.IntroGame);
        data.PlayerOnSceness.Add(SaveGameSlot.Slot2, OnScenes.VampireSurvivor);
        data.PlayerOnSceness.Add(SaveGameSlot.Slot3, OnScenes.IntroGame);
        data.IsSelectedDifficult.Add(SaveGameSlot.Slot1, false);
        data.IsSelectedDifficult.Add(SaveGameSlot.Slot2, false);
        data.IsSelectedDifficult.Add(SaveGameSlot.Slot3, false);


        return data;
    }
    public static float GetValueGameDifficult(TypeLevelDifficult type, TypeControlDifficult typeAttribute)
    {
        return GameConfig.GameDifficult[type][typeAttribute].value;
    }
}
