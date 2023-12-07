using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GameManager : SerializedMonoBehaviour
{
    public enum Script
    {
        GameManager
    }
    public SpaceState SpaceState;
    [SerializeField]private BaseStartGame BaseStartGame = null;
    [SerializeField] Dictionary<int, IOrderable> OrderSoft = new Dictionary<int, IOrderable>();
    public float num = 0;
    private void Awake()
    {
        BaseStartGame = ConfigDataHelper.SaveGame;
        EventDispatcher.Register(Script.GameManager, Events.BaseStartGame, () => BaseStartGame);
        for (int i = 0; i < OrderSoft.Count; i++) 
        {
            if (!OrderSoft.ContainsKey(i))
                continue;
            OrderSoft[i].Init();
        }
    }

    private void Start()
    {

    }

    private void ChestBonus()
    {
        ChestBonus obj = Resources.Load<ChestBonus>("Chests/Chest_Wood");
        Instantiate(obj);
    }


}

