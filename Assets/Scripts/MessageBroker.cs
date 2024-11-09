using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBroker : Singleton<MessageBroker>
{
    public class EventArgs
    {

    }

    public enum EventType
    {
        PlayerDeath,
        PlayerSpawned
    }

    public EventMap<EventType, EventArgs> Events {get; private set;} = new EventMap<EventType, EventArgs>();
}
