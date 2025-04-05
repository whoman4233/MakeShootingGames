using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter.Event
{
    public enum PlayerEventType
    {
        OnMoveStart, OnMoveStop, OnAttack
    }

    public class PlayerEventBus : IEventBus<PlayerEventType>
    {

        private static readonly IDictionary<PlayerEventType, UnityEvent> Events = new Dictionary<PlayerEventType, UnityEvent>();
        private static readonly IDictionary<PlayerEventType, UnityEvent<KeyCode>> ParamEvents = new Dictionary<PlayerEventType, UnityEvent<KeyCode>>();



        public void Subscribe(PlayerEventType playerEventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(playerEventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(playerEventType, thisEvent);
            }
        }
        public void ParamSubscribe(PlayerEventType playerEventType, UnityAction<KeyCode> listener)
        {
            UnityEvent thisEvent;

            if (ParamEvents.ContainsKey(playerEventType))
            {
                ParamEvents[playerEventType].AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                ParamEvents[playerEventType].AddListener(listener);
                ParamEvents[playerEventType].Add(thisEvent);
            }
        }

        public void Unsubscribe(PlayerEventType playerEventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(playerEventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void Publish(PlayerEventType playerEventType)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(playerEventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        public void Publish(PlayerEventType playerEventType, KeyCode keyCode)
        {
            UnityEvent thisEvent;

            if(ParamEvents.TryGetValue(playerEventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}
