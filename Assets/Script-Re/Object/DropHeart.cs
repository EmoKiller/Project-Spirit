using UnityEngine;

public class DropHeart : DropAniDotWeen
{
    public EnemGrPriteHeart Type;
    private void OnTriggerEnter(Collider other)
    {
        EventOnTrigger(other.transform);
    }
    protected override void Event()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.CreateNewHeart, Type);
    }

}
