using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour , IPool
{
    public EnemGrPriteHeart TypeHearts;
    public HeartType heartType;
    [SerializeField] Image imageHeart;
    [SerializeField] RectTransform rectHeart;
    [SerializeField] float Current = 0;

    public string objectName => GetType().Name;
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
        if (TypeHearts == EnemGrPriteHeart.RedHalf || TypeHearts == EnemGrPriteHeart.AddHalf || TypeHearts == EnemGrPriteHeart.BlueHalf)
        {
            heartType = HeartType.Half;
        }
        else
            heartType = HeartType.Full;
        
        Current = (int)heartType;
        UpdateSpriteHeart(TypeHearts, (HeartInfo)Current);
    }
    public float ReturnCurrent()
    {
        return Current;
    }
    public void TakeDamage()
    {
        Current--;
        UpdateSpriteHeart(TypeHearts, (HeartInfo)Current);
    }
    public void RestoreHeart()
    {
        Current++;
        UpdateSpriteHeart(TypeHearts, (HeartInfo)Current);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ObjectPooling.Instance.HeartObj.Remove(this);
    }

    public void Hide()
    {
        ObjectPooling.Instance.PushToPool(this,ObjectPooling.Instance.HeartObj);
        gameObject.SetActive(false);
    }
}
