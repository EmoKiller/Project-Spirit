using DG.Tweening;

public class ObjDropExp : ObjectDropOnWorld, IPool
{
    public int NumEXP = 1;
    public string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        InfomationPlayerManager.Instance.CurrentExp += NumEXP;
    }
    public void Show()
    {
        gameObject.SetActive(true);
        transform.DOMoveY(4, 1).OnComplete(() =>
        {
            Ontrigger = true;
        });
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
