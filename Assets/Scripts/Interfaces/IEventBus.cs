using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter.Event
{
    public interface IEventBus<TEventType>
    {
        public void Subscribe(TEventType eventType, UnityAction listener);
        public void Unsubscribe(TEventType eventType, UnityAction listener);
        public void Publish(TEventType eventType);
    }
    public interface IEventBus<TEventType, TParam>
    {
        public void Subscribe(TEventType eventType, UnityAction<TParam> listener);
        public void Unsubscribe(TEventType eventType, UnityAction<TParam> listener);
        public void Publish(TEventType eventType, TParam param);
    }
}