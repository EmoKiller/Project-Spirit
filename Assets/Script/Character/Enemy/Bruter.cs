using UnityEngine;

public class Bruter : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        EventDispatcher.Addlistener(ListScript.Bruter,Events.TriggerAction, SetAniKill);
    }
    private void SetAniKill()
    {
        _animator.SetTrigger("Kill");
    }
    public void InvokeAction()
    {
        EventDispatcher.Publish(ListScript.Bruter, Events.TriggerAction2);
        Destroy(gameObject);
    }
}
