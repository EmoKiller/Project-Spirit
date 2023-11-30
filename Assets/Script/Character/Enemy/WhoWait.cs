using UnityEngine;

public class WhoWait : MonoBehaviour
{
    public enum Script
    {
        WhoWait
    }
    [SerializeField] Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        EventDispatcher.Addlistener(Script.WhoWait, Events.WhoWaitTriggerAni, SetAni);
    }
    private void SetAni()
    {
        _animator.SetTrigger("Kill");
    }
    public void InvokeAction()
    {
        EventDispatcher.Publish(IntroGame.Script.IntroGame, Events.GoToMap1);
        EventDispatcher.Publish(IntroGame.Script.IntroGame, Events.SetVideoIntro, true);
    }
}
