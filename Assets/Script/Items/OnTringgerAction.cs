using UnityEngine;

public class OnTringgerAction : MonoBehaviour
{
    protected string text = "";
    protected bool actioned = false;
    private void OnTriggerEnter(Collider other)
    {
        if (actioned)
            return;
        EventDispatcher.AddListener(Events.OnPlayerActionItemsButtonDown, ItemAction);
        UIManager.UpdateTextButton.Invoke(text);
    }
    private void OnTriggerExit(Collider other)
    {
        EventDispatcher.RemoveListener(Events.OnPlayerActionItemsButtonDown, ItemAction);
        EventDispatcher.TriggerEvent(Events.SetDefaultButton);
    }
    public virtual void ItemAction()
    {
    }
}
