using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UiControllerHearts : MonoBehaviour
{
    [SerializeField] int numberHp;
    [SerializeField] int Heart;
    [SerializeField] int currentHp;
    [SerializeField] List<GrHeart> grHeart = new List<GrHeart>();

    private void CreateNewHeart(EnemGrHeart grHearts, EnemGrPriteHeart grSprite)
    {
        GameObject obj = Addressables.LoadAssetAsync<GameObject>(GameConstants.UIHeart).WaitForCompletion();
        UIHeart uiHeart = obj.GetComponent<UIHeart>();
        uiHeart.SetNewTypeHeart(grSprite);

        GameObject obj2 = Instantiate(obj, grHeart[(int)grHearts].rectGr);
        grHeart[(int)grHearts].heart.Add(obj2);
        grHeart[(int)grHearts].rectGr.sizeDelta = new Vector2(grHeart[(int)grHearts].rectGr.sizeDelta.x + 45, 0);
    }
    public void UpdateHearts()
    {
        currentHp = numberHp;
        Heart = currentHp / 2;
        if (numberHp % 2 == 0)
            UpdateHeart();
        else
        {
            UpdateHeart();
            CreateNewHeart(EnemGrHeart.Red, EnemGrPriteHeart.RedHalf);
        }
    }
    private void UpdateHeart()
    {
        for (int i = 0; i < Heart; i++)
        {
            CreateNewHeart(EnemGrHeart.Red, EnemGrPriteHeart.Red);
        }
    }
    public void TakeDamage()
    {
        bool isTake = false;
        for (int i = 3; i > -1; i--)
        {
            for (int j = grHeart[i].heart.Count; j > 0; j--)
            {
                UIHeart scr = grHeart[i].heart[j - 1].GetComponent<UIHeart>();
                if (scr.ReturnCurrent() != 0) 
                {
                    scr.TakeDamage(1);
                    
                    isTake = true;
                    break;
                }
            }
            if (isTake)
                break;
        }
    }
}
public enum UIsizeObj
{
    Nomal,
    Large,
    Half
}
public enum EnemGrHeart
{
    Red,
    Add,
    Blue,
    Black
}
public enum EnemGrPriteHeart
{
    Red,
    RedHalf,
    Add,
    AddHalf,
    Blue,
    BlueHalf,
    Black
}
public enum HeartType
{
    Half = 1,
    Full = 2
}
public enum HeartInfo
{
    Empty,
    Half,
    Full
}
