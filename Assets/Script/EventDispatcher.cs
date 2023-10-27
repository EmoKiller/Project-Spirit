using System;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatcherComponent : MonoBehaviour
{
    private static EventDispatcherComponent instance;

    private Dictionary<string, Action<object>> _eventDictionary = new Dictionary<string, Action<object>>();
    
    public static EventDispatcherComponent Instance()
    {
        if (instance == null)
            instance = new EventDispatcherComponent();
        return instance;
    }

    private void AddEventListener(string eventName, Action<object> listener)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent += listener;
            _eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            _eventDictionary.Add(eventName, thisEvent);
        }
    }

    private void RemoveEventListener(string eventName, Action<object> listener)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null)
            {
                _eventDictionary.Remove(eventName);
            }
            else
            {
                _eventDictionary[eventName] = thisEvent;
            }
        }
    }

    private void DispatchEvent(string eventName, object payload = null)
    {
        if (_eventDictionary.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent.Invoke(payload);
        }
    }
    private void OnDisable()
    {
        instance = null;
    }
    public void TestDispatcher()
    {
        Debug.Log("TestDispatcher");
    }

}