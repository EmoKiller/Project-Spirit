using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dungeon Infomation")]
    public string NameDungeon = "";
    public string NamePhase = "";
    private void Awake()
    {

    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
    }

    private void ChestBonus()
    {
        ChestBonus obj = Resources.Load<ChestBonus>("Chests/Chest_Wood");
        Instantiate(obj);
    }


}

