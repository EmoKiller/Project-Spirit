using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static void DelayCall(this MonoBehaviour mono, float time, Action callBack)
    {
        mono.StartCoroutine(IEDelayCall(time,callBack));
    }
    public static IEnumerator IEDelayCall(float time, Action callBack)
    {
        yield return new WaitForSeconds(time);
        callBack?.Invoke();
    }
    public static void LoopDelayCall(this MonoBehaviour mono, float time, Action callBack)
    {
        mono.StartCoroutine(IELoopDelayCall(time, callBack));
    }
    public static IEnumerator IELoopDelayCall(float time, Action callBack)
    {
        float start = 0f;
        while (start <= time)
        {
            start += Time.deltaTime;
            callBack?.Invoke();
        }
        yield return null;
        
    }
    public static T TryGetMonoComponent<T>(this MonoBehaviour mono, ref T tryValue)
    {
        if(tryValue == null)
            tryValue = mono.GetComponent<T>();
        return tryValue;
    }
}
