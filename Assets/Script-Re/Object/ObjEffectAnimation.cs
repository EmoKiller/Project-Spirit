using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjEffectAnimation : MonoBehaviour, IPool
{
    [SerializeField] Animator _animator;
    public string objectName => gameObject.name;
    public void Hide()
    {
        RewardSystem.Instance.RemoveFromListObjEffectAnimation(this);
        ObjectPooling.Instance.PushToPoolObjEffectAnimation(this);
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        ObseverConstants.ReloadScene.AddListener(Hide);
    }
    private void OnDisable()
    {
        ObseverConstants.ReloadScene.RemoveListener(Hide);
    }
}
