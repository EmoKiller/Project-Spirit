using DG.Tweening;
using UnityEngine;

public class ObjDropExp : ObjectDropOnWorld, IPool
{
    public string objectName => GetType().Name;

    protected override void PublishEvent()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UpdateValueExp, 1f);
        //uplevel
    }
    protected override void Event()
    {
        base.Event();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        Ontrigger = true;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
