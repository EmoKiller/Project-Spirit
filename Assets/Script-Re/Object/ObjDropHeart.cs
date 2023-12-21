using UnityEngine;

public class ObjDropHeart : ObjectDropOnWorld , IPool
{
    [Header("TypeHeart")]
    public EnemGrPriteHeart TypeHeart;
    public string objectName => GetType().Name;

    protected override void OnTriggerEnter(Collider other)
    {
        if (InfomationPlayerManager.Instance.CompareCurrentNMaxAttributes(GameUtilities.ConvertGrSpriteToAttributeCurrent(TypeHeart), GameUtilities.ConvertGrSpriteToAttributeMax(TypeHeart)))
        {
            return;
        }
        base.OnTriggerEnter(other);
    }
    protected override void PublishEvent()
    {
        InfomationPlayerManager.Instance.IncreaseValueOf(GameUtilities.ConvertGrSpriteToAttributeCurrent(TypeHeart), GameUtilities.ConvertInt(TypeHeart));
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
