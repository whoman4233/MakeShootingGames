using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter.Event
{
    public enum EnemyEventType
    {
        Dead, SpawnBoss
    }

    public class EnemyEventBus : IEventBus<EnemyEventType>
    {
        private static readonly IDictionary<EnemyEventType, UnityEvent> Events = new Dictionary<EnemyEventType, UnityEvent>();

        public void Subscribe(EnemyEventType enemyEventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(enemyEventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(enemyEventType, thisEvent);
            }
        }

        public void Unsubscribe(EnemyEventType enemyEventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(enemyEventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public void Publish(EnemyEventType enemyEventType)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(enemyEventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}