using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter.Event
{
    public enum GameEventType
    {
        Lobby, Start, End, UIBackButton, UINextButton, UIStartButton, UIClearButton  , UIRegameButton
    }

    public class GameEventBus : IEventBus<GameEventType>
    {
        private static readonly IDictionary<GameEventType, UnityEvent> Events = new Dictionary<GameEventType, UnityEvent>();

        public void Subscribe(GameEventType gameEventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if(Events.TryGetValue(gameEventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(gameEventType, thisEvent);
            }
        }

        public void Unsubscribe(GameEventType gameEventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if(Events.TryGetValue(gameEventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void Publish(GameEventType gameEventType)
        {
            UnityEvent thisEvent;

            if(Events.TryGetValue(gameEventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    } 
}
