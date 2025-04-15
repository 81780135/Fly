using System;
using System.Collections.Generic;
using UnityEngine;
public static class EventBus
{
    // 事件类型 → 回调列表（支持泛型）
    private static Dictionary<Type, List<Delegate>> eventHandlers = new Dictionary<Type, List<Delegate>>();

    // 订阅事件
    public static void Subscribe<T>(Action<T> handler) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (!eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = new List<Delegate>();
        }
        eventHandlers[eventType].Add(handler);
    }

    // 取消订阅事件
    public static void Unsubscribe<T>(Action<T> handler) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (eventHandlers.TryGetValue(eventType, out var handlers))
        {
            handlers.Remove(handler);
            if (handlers.Count == 0)
            {
                eventHandlers.Remove(eventType);
            }
        }
    }

    // 发布事件
    public static void Publish<T>(T eventArgs) where T : EventArgs
    {
        Type eventType = typeof(T);
        if (eventHandlers.TryGetValue(eventType, out var handlers))
        {
            // 复制列表以避免迭代时修改导致的异常
            foreach (Delegate handler in handlers.ToArray())
            {
                ((Action<T>)handler)?.Invoke(eventArgs);
            }
        }
    }

    // 清空所有订阅（用于场景切换时调用）
    public static void Clear()
    {
        eventHandlers.Clear();
    }

    public static void Subscribe<T>(Action<T> handler, int priority = 0) where T : EventArgs
    {
        // 按优先级排序处理
    }

    public static void PublishAsync<T>(T eventArgs) where T : EventArgs
    {
        // 在后台线程触发事件
    }

    public static void LogSubscriptions()
    {
        foreach (var pair in eventHandlers)
        {
            Debug.Log($"事件类型：{pair.Key.Name}，订阅数：{pair.Value.Count}");
        }
    }
}