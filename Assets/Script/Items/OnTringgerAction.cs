using UnityEngine;

public class OnTringgerAction : MonoBehaviour
{
    protected string text = "";
    protected string imageButton = "";
    protected bool actioned = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (actioned)
            return;
        EventDispatcher.Publish(ListScript.UIManager, Events.UpdateText,text);
        EventDispatcher.Publish(ListScript.UIManager, Events.AddListener);
        EventDispatcher.Publish(ListScript.UIManager, Events.SwitchImageButton,imageButton);
        //EventDispatcher.AddListener(Events.FillAmount, CallFillAmiunt);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        EventDispatcher.Publish(ListScript.UIManager, Events.SetDefaultButton);
        EventDispatcher.Publish(ListScript.UIManager, Events.RemoveEvent);
    }
    public virtual void ItemAction()
    {

    }

    public void CallFillAmiunt(int value)
    {
        float fillAmiunt = value;
    }
}
