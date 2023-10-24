using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UnityEventGeneric : UnityEvent { }
public class UnityEventGeneric<T1> : UnityEvent<T1> { }
public class UnityEventGeneric<T1, T2> : UnityEvent<T1, T2> { }
public class EventDispatcher2 : MonoBehaviour
{
    


    public static Dictionary<Events, UnityEvent> _events = new Dictionary<Events, UnityEvent>();

    public static void AddListener(Events eventName, UnityAction Action)
    {
        if (!_events.ContainsKey(eventName))
            _events.Add(eventName, new UnityEvent());

        _events[eventName].AddListener(Action);
    }
    //public static void AddListener(Events eventName, UnityAction Action)
    //{
    //    if (!_events.ContainsKey(eventName))
    //        _events.Add(eventName, new UnityEventGeneric<T1>());

    //    _events[eventName].AddListener(Action);
    //}

    public static void RemoveListener(Events eventName, UnityAction Action)
    {
        if (!_events.ContainsKey(eventName))
            return;

        _events[eventName].RemoveListener(Action);
    }

    public static void RemoveAllListener(Events eventName)
    {
        if (!_events.ContainsKey(eventName))
            return;

        _events[eventName].RemoveAllListeners();
    }

    public static void TriggerEvent(Events eventName)
    {
        if (!_events.ContainsKey(eventName))
            return;

        _events[eventName]?.Invoke();
    }
}
