using DG.Tweening;

public class ObjDropExp : ObjectDropOnWorld
{
    public float NumEXP = 1;
    public override string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        InfomationPlayerManager.Instance.CurrentExp += NumEXP;
    }
    public override void Show()
    {
        gameObject.SetActive(true);
        transform.DOMoveY(4, 1).OnComplete(() =>
        {
            Ontrigger = true;
        });
    }
}
