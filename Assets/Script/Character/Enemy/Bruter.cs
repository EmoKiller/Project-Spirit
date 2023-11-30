using UnityEngine;

public class Bruter : MonoBehaviour
{
    public enum Script
    {
        Bruter
    }
    [SerializeField] Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        EventDispatcher.Addlistener(Script.Bruter,Events.BruterTriggerAni, SetAniKill);
    }
    private void SetAniKill()
    {
        _animator.SetTrigger("Kill");
    }
    public void InvokeAction()
    {
        EventDispatcher.Publish(IntroGame.Script.IntroGame, Events.GoToMap2);
        Destroy(gameObject);
    }
}
