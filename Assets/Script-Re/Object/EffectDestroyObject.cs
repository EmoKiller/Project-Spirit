using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectDestroyObject : MonoBehaviour
{
    [SerializeField]List<GameObject> gameObjects = new List<GameObject>();
    public void Rotate()
    {

    }
    public void DesTroyObject()
    {
        gameObject.SetActive(true);
        foreach (GameObject obj in gameObjects)
        {
            float numx = Random.Range(1f, 5f);
            float numy = Random.Range(0f, -0.2f);
            float numz = Random.Range(-2f, 2f);
            
            obj.transform.DOLocalMove(new Vector3(numx, numy, numz), 0.3f).OnComplete(() =>
            {
                obj.transform.DOLocalJump(new Vector3(numx+1, -1.8f, numz), 0.1f, 1, 0.5f);
            });
        }
    }
    public void ResetTran()
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
        }
    }
}

