
using System.Collections.Generic;
using UnityEngine.Events;

public static class EventDispatcher
{
    public static Dictionary<Events, UnityEvent> _events = new Dictionary<Events, UnityEvent>();

    public static void AddListener(Events eventName, UnityAction Action)
    {
        if (!_events.ContainsKey(eventName))
            _events.Add(eventName, new UnityEvent());

        _events[eventName].AddListener(Action);
    }

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


