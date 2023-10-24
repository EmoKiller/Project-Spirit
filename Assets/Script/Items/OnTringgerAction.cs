using UnityEngine;

public class OnTringgerAction : MonoBehaviour
{
    protected string text = "";
    protected bool actioned = false;
    private void OnTriggerEnter(Collider other)
    {
        if (actioned)
            return;
        EventDispatcher.AddListener(Events.OnPlayerActionItems, ItemAction);
        UIManager.UpdateStringButtonE.Invoke(text);
    }
    private void OnTriggerExit(Collider other)
    {
        EventDispatcher.RemoveListener(Events.OnPlayerActionItems, ItemAction);
        EventDispatcher.TriggerEvent(Events.OnTriggerItems);
    }
    public virtual void ItemAction()
    {
    }
}
