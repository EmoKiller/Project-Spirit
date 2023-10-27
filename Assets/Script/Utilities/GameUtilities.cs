using System;
using System.Collections;
using System.Collections.Generic;
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
    public static void LoopDelayCall(this MonoBehaviour mono,NavMeshAgent agent, float time, Action callBack)
    {
        mono.StartCoroutine(IELoopDelayCall(agent,time, callBack));
    }
    public static IEnumerator IELoopDelayCall(NavMeshAgent agent, float time, Action callBack)
    {
        Vector3 destination = new Vector3(5f, 0f, 5f);
        agent.SetDestination(destination);


        //while (!navMeshAgent.pathPending)
        //{
        //    if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        //    {
        //        if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
        //        {
        //            Debug.Log("Reached the destination.");
        //            break;
        //        }
        //    }
        //    yield return null;
        //}
        yield return null;
    }

}
