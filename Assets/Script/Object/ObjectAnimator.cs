using System;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    public Action ActionOne = null;
    public void Action1()
    {
        ActionOne?.Invoke();
    }
}
