using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyObject : MonoBehaviour , IPool
{
    [SerializeField]List<GameObject> gameObjects = new List<GameObject>();
    public ListTypeEffects effects;
    public string objectName => effects.ToString();
    public void DesTroyObject()
    {

        gameObject.SetActive(true);
        foreach (GameObject obj in gameObjects)
        {
            this.DelayCall(2, () =>
            {
                if (isActiveAndEnabled == false)
                    return;
                Hide();
            });
            float numx = Random.Range(1f, 5f);
            float numy = Random.Range(0f, -0.2f);
            float numz = Random.Range(-2f, 2f);
            
            obj.transform.DOLocalMove(new Vector3(numx, numy, numz), 0.3f).OnComplete(() =>
            {
                if (isActiveAndEnabled == false)
                {
                    DOTween.KillAll();
                }
                obj.transform.DOLocalJump(new Vector3(numx + 1, -1.8f, numz), 0.1f, 1, 0.5f);
            });
            
        }
    }
    public void ResetTransform()
    {
        transform.DORotate(Vector3.zero, 0);
        for (int i = 0; i < gameObjects.Count - 1; i++)
        {
            gameObjects[i].transform.localPosition = Vector3.zero;
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
        DesTroyObject();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        RewardSystem.Instance.RemoveFromListEffectDestroyObj(this);
        ObjectPooling.Instance.PushToPoolEffectDestroyObj(this);
        ResetTransform();
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

