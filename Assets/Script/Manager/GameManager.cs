using UnityEngine;

public class GameManager : MonoBehaviour
{
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

