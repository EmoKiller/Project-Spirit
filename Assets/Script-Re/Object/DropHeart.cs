using UnityEngine;

public class DropHeart : DropAniDotWeen
{
    public EnemGrPriteHeart Type;
    private bool isTake = false;
    private void Start()
    {
        isTake = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        EventOnTrigger();
    }
    protected override void Event()
    {
        transform.position = Vector3.LerpUnclamped(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position, 10 * Time.deltaTime);
        if (Vector3.Distance(transform.position, ((Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerTransform)).position) < 0.5f && isTake == false)
        {
            EventDispatcher.Publish(UIManager.Script.UIManager, Events.CreateNewHeart, Type);
            isTake = true;
            gameObject.SetActive(false);
        }
    }

}
