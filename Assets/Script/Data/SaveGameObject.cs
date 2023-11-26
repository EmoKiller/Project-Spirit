using UnityEngine;
[CreateAssetMenu(fileName = "NewSaveGame", menuName = "GameUtilities/SaveGame")]
public class SaveGameObject : ScriptableObject
{
    [Header("Comfinguration")]
    public int level = 0;
    public int Exp = 0;
    public int AmountFollowers = 0;
    public int AmountOfCoin = 0;

    public SaveGameObject() { }
    public void ShowInfomation()
    {
        Debug.Log("Level: " + level);
        Debug.Log("Exp: " + Exp);
        Debug.Log("AmountFollowers: " + AmountFollowers);
        Debug.Log("AmountOfCoin: " + AmountOfCoin);
    }
    public void ResetAttribute()
    {
        level = 0;
        Exp = 0;
        AmountFollowers = 0;
        AmountOfCoin = 0;
    }
    
}
