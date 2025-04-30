using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chapter.Event
{
    public enum PlayerEventType
    {
        OnMoveStart, OnMoveStop, OnAttack, OnHit, OnDead
    }

    public class PlayerEventBus : IEventBus<PlayerEventType>, IEventBus<PlayerEventType, KeyCode>
    {
        //�÷��̾� �ൿ �� ������ �������� ����� �ൿ�� Ű�Է��� �޴°� �ֱ� ������ �Ķ���Ͱ����� ������ ���� �̺�Ʈ ���� 2���� ���
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
        public void Subscribe(PlayerEventType playerEventType, UnityAction<KeyCode> listener)
        {
            UnityEvent<KeyCode> thisEvent;

            if (ParamEvents.TryGetValue(playerEventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent<KeyCode>();
                thisEvent.AddListener(listener);
                ParamEvents.Add(playerEventType, thisEvent);
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

        public void Unsubscribe(PlayerEventType playerEventType, UnityAction<KeyCode> listener)
        {
            UnityEvent<KeyCode> thisEvent;

            if(ParamEvents.TryGetValue(playerEventType, out thisEvent))
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
            UnityEvent<KeyCode> thisEvent;

            if (ParamEvents.TryGetValue(playerEventType, out thisEvent))
            {
                thisEvent.Invoke(keyCode);
            }
        }
    }
}
