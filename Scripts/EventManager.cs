using System;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Custom event types for the event manager
/// </summary>
public enum CustomEventType
{
    ExampleEvent,
    ExampleEvent2,
    ExampleEvent3
}


/// <summary>
/// Handles the events passed between objects
/// </summary>
public class EventManager
{

    private static EventManager instance = new();
    private Hashtable eventHash = new();

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }

            return instance;
        }
    }

    public static void StartListening<T>(CustomEventType eventType, UnityAction<T> listener)
    {
        UnityEvent<T> thisEvent = null;

        string key = GetKey<T>(eventType);

        if (instance.eventHash.ContainsKey(key))
        {
            thisEvent = (UnityEvent<T>)instance.eventHash[key];
            thisEvent.AddListener(listener);
            instance.eventHash[eventType] = thisEvent;
        }
        else
        {
            thisEvent = new UnityEvent<T>();
            thisEvent.AddListener(listener);
            instance.eventHash.Add(key, thisEvent);
        }
    }

    public static void StartListening(CustomEventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if (instance.eventHash.ContainsKey(eventType))
        {
            thisEvent = (UnityEvent)instance.eventHash[eventType];
            thisEvent.AddListener(listener);
            instance.eventHash[eventType] = thisEvent;
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventHash.Add(eventType, thisEvent);
        }
    }

    private static string GetKey<T>(CustomEventType eventType)
    {
        Type type = typeof(T);
        string key = type.ToString() + eventType.ToString();
        return key;
    }

    public static void StopListening<T>(CustomEventType eventType, UnityAction<T> listener)
    {
        if (instance == null) return;

        UnityEvent<T> thisEvent = null;

        string key = GetKey<T>(eventType);

        if (instance.eventHash.ContainsKey(key))
        {
            thisEvent = (UnityEvent<T>)instance.eventHash[key];
            thisEvent.RemoveListener(listener);
            instance.eventHash[eventType] = thisEvent;
        }
    }

    public static void StopListening(CustomEventType eventType, UnityAction listener)
    {
        if (instance == null) return;

        UnityEvent thisEvent = null;

        if (instance.eventHash.ContainsKey(eventType))
        {
            thisEvent = (UnityEvent)instance.eventHash[eventType];
            thisEvent.RemoveListener(listener);
            instance.eventHash[eventType] = thisEvent;
        }
    }

    public static void Invoke<T>(CustomEventType eventType, T value)
    {
        UnityEvent<T> thisEvent = null;

        string key = GetKey<T>(eventType);

        if (instance.eventHash.ContainsKey(key))
        {
            thisEvent = (UnityEvent<T>)instance.eventHash[key];
            thisEvent.Invoke(value);
        }
    }

    public static void Invoke(CustomEventType eventType)
    {
        UnityEvent thisEvent = null;

        if (instance.eventHash.ContainsKey(eventType))
        {
            thisEvent = (UnityEvent)instance.eventHash[eventType];
            thisEvent.Invoke();
        }
    }
}