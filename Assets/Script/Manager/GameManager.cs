using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SaveGameObject SaveGameObject;
    public SpaceState SpaceState;
    public string NameDungeon = "";
    public string NamePhase = "";

    private void OnEnable()
    {
        
    }
    private void Awake()
    {
    }
    private void Start()
    {
        SaveGameObject.ShowInfomation();
        if (ES3.KeyExists("SaveGame2"))
        {
            Debug.Log("nullssss");

        }
        if (!ES3.KeyExists("SaveGame1"))
        {
            Debug.Log("null");
            
        }
        if (ES3.KeyExists("SaveGame1"))
        {
            Debug.Log("savegame1");
            SaveGameObject loadedPlayer = ES3.Load<SaveGameObject>("SaveGame1");
            loadedPlayer.level = 0;
            loadedPlayer.ShowInfomation();
        }
        //else
        //{
        //    ES3.Save("SaveGame1", SaveGameObject);
        //}
        ES3.DeleteKey("SaveGame1");

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

