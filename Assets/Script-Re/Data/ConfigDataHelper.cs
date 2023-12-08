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
    /// <summary>
    /// save game for RoomTest
    /// </summary>
    private static BaseStartGame baseStartGame = null;
    public static BaseStartGame BaseStartGame
    {
        get
        {
            if (!ES3.KeyExists(GameConstants.BaseStartGame))
            {
                ES3.Save(GameConstants.BaseStartGame, baseStartGame);
            }
            else
            {
                baseStartGame = ES3.Load<BaseStartGame>(GameConstants.BaseStartGame);
            }
            return baseStartGame;
        }
        set => ES3.Save(GameConstants.BaseStartGame, value);
    }
    public static BaseStartGame GetStartGameDataBase()
    {
        BaseStartGame data = BaseStartGame;

        return data;
    }

    //private static UserData GetHeroClassData()
    //{
    //    UserData user = new UserData();
    //    user.data.Add("Hero1", GetHeroData());
    //    return user;
    //}
    //private static HeroData GetHeroData()
    //{
    //    HeroData hero1 = new HeroData();
    //    //hero1.heroClass = GameConfig.heroConfig.heroClass;
    //    //Debug.Log(hero1.heroClass.Values);
    //    //hero1.attributes = GameConfig.heroConfig.heroClass.
    //    return hero1;
    //}
    //public void Save(Dictionary<string,string> data)
    //{
    //    //PlayerPrefs.se
    //}
}
