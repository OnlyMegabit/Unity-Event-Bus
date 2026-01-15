using System;
using System.Collections.Generic;

public static class EventBus
{
    // Key: The name of the event (string)
    // Value: The action to perform (Action)
    private static readonly Dictionary<string, Action> Events = new Dictionary<string, Action>();

    // SUBSCRIBE: Start listening for this event
    public static void Subscribe(string eventName, Action listener)
    {
        if (!Events.ContainsKey(eventName))
        {
            Events[eventName] = null;
        }
        Events[eventName] += listener;
    }

    // UNSUBSCRIBE: Stop listening
    public static void Unsubscribe(string eventName, Action listener)
    {
        if (Events.ContainsKey(eventName))
        {
            Events[eventName] -= listener;
        }
    }

    // PUBLISH: Broadcast the event to everyone listening
    public static void Publish(string eventName)
    {
        if (Events.ContainsKey(eventName))
        {
            Events[eventName]?.Invoke();
        }
    }
}