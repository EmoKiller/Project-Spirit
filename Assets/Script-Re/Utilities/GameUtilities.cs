using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public static void WaitDelayCall(this MonoBehaviour mono,int repeat, float waittime, Action callBack)
    {
        mono.StartCoroutine(IEWaitDelayCall(repeat, waittime, callBack));
    }
    public static IEnumerator IEWaitDelayCall(int repeat, float waittime, Action callBack)
    {
        int Current = 0;
        while (Current < repeat)
        {
            callBack?.Invoke();
            Current++;
            yield return new WaitForSeconds(waittime);
        }
    }

    public static void ReSetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.eulerAngles = new Vector3(15,0,0);
    }
    public static T TryGetMonoComponent<T>(this MonoBehaviour mono, ref T tryValue)
    {
        if (tryValue == null)
            tryValue = mono.GetComponent<T>();
        return tryValue;
    }
    public static T TryGetMonoComponentInChildren<T>(this MonoBehaviour mono, ref T tryValue)
    {
        if (tryValue == null)
            tryValue = mono.GetComponentInChildren<T>();
        return tryValue;
    }
    public static List<T> TryGetMonoComponentsInChildren<T>(this MonoBehaviour mono, ref List<T> tryValue)
    {
        if (tryValue == null)
            tryValue = mono.GetComponentsInChildren<T>().ToList();
        return tryValue;
    }
}