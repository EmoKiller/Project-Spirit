using UnityEngine;

public class ObjDropHeart : ObjectDropOnWorld
{
    [Header("TypeHeart")]
    public EnemGrPriteHeart TypeHeart;
    public override string objectName => GetType().Name;
    private void Awake()
    {
        ObseverConstants.ReloadScene.AddListener(Hide);
    }
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
    public override void Show()
    {
        gameObject.SetActive(true);
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}
