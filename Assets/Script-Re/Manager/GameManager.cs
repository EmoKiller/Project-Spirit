using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpaceState SpaceState;
    public string NameDungeon = "";
    public string NamePhase = "";
    [SerializeField]private SaveGameObject SaveGameObject;
    //private SaveGames SaveGames = null;
    private void Awake()
    {
        
    }
    private void Start()
    {
        SaveGameObject.ShowInfomation();
        ////SaveGames = ConfigDataHelper.SaveGame;
        //foreach (var index in ConfigDataHelper.GameConfig.SaveGames)
        //{
        //    Debug.Log(index.Key + " " + index.Value.Value);
        //    index.Value.Value += 1;
        //}
        //foreach (var index in ConfigDataHelper.GameConfig.SaveGames)
        //{
        //    Debug.Log(index.Key + " " + index.Value.Value);
        //    index.Value.Value += 1;
        //}
        //foreach (var index in ConfigDataHelper.GameConfig.SaveGames)
        //{
        //    Debug.Log(index.Key + " " + index.Value.Value);
        //}
        //string json = JsonConvert.SerializeObject(ConfigDataHelper.GameConfig);
        //System.IO.File.WriteAllText("Assets/Resources/Data/GameConfig.json", json);
        //JsonUtility.ToJson(json);
    }
    private void Update()
    {
        
    }
    
    private void ChestBonus()
    {
        ChestBonus obj = Resources.Load<ChestBonus>("Chests/Chest_Wood");
        Instantiate(obj);
    }


}

