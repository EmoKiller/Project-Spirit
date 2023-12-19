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
            if (!ES3.KeyExists("HeroDatas"))
            {
                heroData = GetHeroData();
                ES3.Save("HeroDatas", GetHeroData());
            }
            else
            {
                heroData = ES3.Load<HeroData>("HeroDatas");
            }
            return heroData;
        }
        set => ES3.Save("HeroDatas", value);
    }
    private static HeroData GetHeroData()
    {
        HeroData data = new HeroData();

        return data;
    }
}
