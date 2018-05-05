using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystem
{
    public class EventInstance<T>
    {
        public delegate void Handler(T evt);
        Handler m_handlers;
        public static EventInstance<T> instance = new EventInstance<T>();

        public void Subscribe(Handler handler)
        {
            m_handlers += handler;
        }

        public void Unsubscribe(Handler handler)
        {
            m_handlers -= handler;
        }

        public void Fire(T evt)
        {
            m_handlers.Invoke(evt);
        }
    }


    public static void Subscribe<T>(EventInstance<T>.Handler handler)
    {
        EventInstance<T>.instance.Subscribe(handler);
    }

    public static void Unsubscribe<T>(EventInstance<T>.Handler handler)
    {
        EventInstance<T>.instance.Unsubscribe(handler);
    }

    public static void Fire<T>(T evt)
    {
        EventInstance<T>.instance.Fire(evt);
    }
}
