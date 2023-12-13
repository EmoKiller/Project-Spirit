using UnityEngine;

public class CursesPodium : TriggerWaitAction
{
    [SerializeField] CursesEquip Curses = null;
    float damagePlayer = 0;
    float speedPlayer = 0;
    private void Start()
    {
        Curses = GetComponentInChildren<CursesEquip>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.SetDefault);
    }
}
