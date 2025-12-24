using System;
using System.Collections.Generic;
using UnityEngine;

public static class MessageBus
{
    private static readonly Dictionary<Type, Delegate> handlers = new();

    public static void Subscribe<T>(Action<T> handler) where T : IMessage
    {
        if (handlers.TryGetValue(typeof(T), out var del))
            handlers[typeof(T)] = Delegate.Combine(del, handler);
        else
            handlers[typeof(T)] = handler;
    }

    public static void Unsubscribe<T>(Action<T> handler) where T : IMessage
    {
        if (!handlers.TryGetValue(typeof(T), out var del)) return;
        handlers[typeof(T)] = Delegate.Remove(del, handler);
    }

    public static void ClearAction<T>(Action<T> handler) where T : IMessage
    {
        if (!handlers.TryGetValue(typeof(T), out var del)) return;
        handlers[typeof(T)] = null;
    }

    public static void Publish<T>(T message) where T : IMessage
    {
        if (handlers.TryGetValue(typeof(T), out var del))
            (del as Action<T>)?.Invoke(message);
    }
}
