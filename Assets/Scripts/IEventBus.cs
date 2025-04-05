using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter.Event
{
    public interface IEventBus<TEventType>
    {
        void Subscribe(TEventType eventType, UnityAction listener);
        void Unsubscribe(TEventType eventType, UnityAction listener);
        void Publish(TEventType eventType);
    }
    public interface IEventBus<TEventType, TParam>
    {
        void Subscribe(TEventType eventType, UnityAction<TParam> listener);
        void Unsubscribe(TEventType eventType, UnityAction<TParam> listener);
        void Publish(TEventType eventType, TParam param);
    }
}