using DG.Tweening;

public class ObjDropAngry : ObjectDropOnWorld , IPool
{
    public int NumAngry = 1;
    public string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        InfomationPlayerManager.Instance.CurretAngry += NumAngry;
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
