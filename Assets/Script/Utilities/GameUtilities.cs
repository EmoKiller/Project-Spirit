using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
    public static T TryGetMonoComponent<T>(this MonoBehaviour mono, ref T tryValue)
    {
        if(tryValue == null)
            tryValue = mono.GetComponent<T>();
        return tryValue;
    }
    public static void LoopDelayCall(this MonoBehaviour mono, float endtime, Action callBack)
    {
        mono.StartCoroutine(IELoopDelayCall(endtime, callBack));
    }
    public static IEnumerator IELoopDelayCall(float endtime, Action callBack)
    {
        float StartTime = 0;
        while (StartTime <= endtime)
        {
            callBack?.Invoke();
            StartTime += Time.deltaTime;
            yield return null;
        }
    }
}
