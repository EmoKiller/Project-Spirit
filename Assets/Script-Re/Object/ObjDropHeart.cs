using UnityEngine;

public class ObjDropHeart : ObjectDropOnWorld , IPool
{
    [Header("TypeHeart")]
    public EnemGrPriteHeart TypeHeart;
    public string objectName => GetType().Name;

    protected override void OnTriggerEnter(Collider other)
    {
        if ((bool)EventDispatcher.Call(UIManager.Script.UIManager, Events.CheckCurrentHP, GameUtilities.ConvertGrSpriteToGrHeart(TypeHeart)))
        {
            return;
        }
        base.OnTriggerEnter(other);
    }
    protected override void PublishEvent()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.RestoreHeart, GameUtilities.ConvertGrSpriteToGrHeart(TypeHeart), GameUtilities.ConvertInt(TypeHeart));
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
