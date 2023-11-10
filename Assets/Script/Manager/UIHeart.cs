using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIHeart : MonoBehaviour
{
    public EnemGrPriteHeart TypeHearts;
    public HeartType heartType;
    [SerializeField] Image imageHeart;
    [SerializeField] RectTransform rectHeart;
    [SerializeField] float Current = 0;

    public void UpdateHeart()
    {
        
    }
    private void UpdateSpriteHeart(EnemGrPriteHeart typeHearts , HeartInfo heartInfo)
    {
        imageHeart.sprite = Addressables.LoadAssetAsync<Sprite>(string.Format(GameConstants.Hearts, typeHearts, heartInfo + ".asset")).WaitForCompletion();
        if (TypeHearts == EnemGrPriteHeart.RedHalf || TypeHearts == EnemGrPriteHeart.AddHalf)
        {
            rectHeart.sizeDelta = new Vector2(22.5f, 45);
            rectHeart.anchoredPosition = new Vector2(11.25f, 0);
            return;
        }
        rectHeart.sizeDelta = new Vector2(45, 45);
        rectHeart.anchoredPosition = new Vector2(22.5f, 0);
    }
    public void SetNewTypeHeart(EnemGrPriteHeart TypeHearts)
    {
        this.TypeHearts = TypeHearts;
        if (TypeHearts == EnemGrPriteHeart.RedHalf || TypeHearts == EnemGrPriteHeart.AddHalf || TypeHearts == EnemGrPriteHeart.BlueHalf || TypeHearts == EnemGrPriteHeart.Black)
        {
            heartType = HeartType.Half;
        }
        else
            heartType = HeartType.Full;
        
        Current = (float)heartType;
        UpdateSpriteHeart(TypeHearts, (HeartInfo)Current);
    }
    public float ReturnCurrent()
    {
        return Current;
    }
    public void TakeDamage(float dmg)
    {
        Current -= dmg;
        UpdateSpriteHeart(TypeHearts, (HeartInfo)Current);
    }
}
