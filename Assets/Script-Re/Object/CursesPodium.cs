using UnityEngine;

public class CursesPodium : TriggerWaitAction
{
    [SerializeField] CursesEquip Curses = null;
    private void Start()
    {
        Curses = GetComponentInChildren<CursesEquip>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        ShowInfoCursesPodium();
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.SetDefault);
    }
    protected override void OnTringgerWaitAction()
    {
        if (actioned)
            return;
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.SetDefault);
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UpdateIconCurses, Curses.CursesObject.IconCurses);
        EventDispatcher.Publish(Player.Script.Player, Events.PlayerChangeCurses, Curses);
        Curses.gameObject.SetActive(false);
        enabled = false;
        base.OnTringgerWaitAction();
    }
    private void ShowInfoCursesPodium()
    {
        EventDispatcher.Publish(UIManager.Script.UIManager, Events.UpdateInfoCurses,
            Curses.CursesObject.NameCurses,
            Curses.CursesObject.QuoteCurses,
            Curses.CursesObject.DescriptionCurses
            );
    }
}
