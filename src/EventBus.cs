using System;
using System.Collections.Generic;

public static class EventBus<T>
{
    // Key: The name of the event (string)
    // Value: The action to perform (Action)
    private static readonly Dictionary<string, Action<T>> Events = new Dictionary<string, Action<T>>();

    // SUBSCRIBE: Start listening for this event
    public static void Subscribe(string eventName, Action<T> listener)
    {
        if (!Events.ContainsKey(eventName))
        {
            Events[eventName] = null;
        }
        Events[eventName] += listener;
    }

    // UNSUBSCRIBE: Stop listening
    public static void Unsubscribe(string eventName, Action<T> listener)
    {
        if (Events.ContainsKey(eventName))
        {
            Events[eventName] -= listener;
        }
    }

    // PUBLISH: Broadcast the event to everyone listening
    public static void Publish(string eventName, T data)
    {
        Events.TryGetValue(eventName, out Action<T> action);
        action?.Invoke(data);
    }

    // CLEAR: Useful when switching scenes
    public static void ClearAll()
    {
        Events.Clear();
    }
}
