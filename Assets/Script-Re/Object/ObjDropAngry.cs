using DG.Tweening;

public class ObjDropAngry : ObjectDropOnWorld 
{
    public int NumAngry = 1;
    public override string objectName => GetType().Name;
    protected override void PublishEvent()
    {
        InfomationPlayerManager.Instance.CurrentAngry += NumAngry;
    }
}
