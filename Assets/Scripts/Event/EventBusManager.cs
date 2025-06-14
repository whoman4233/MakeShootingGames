using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Event;
using Chapter.Singleton;

namespace Chapter.Manager
{
    public class EventBusManager : Singleton<EventBusManager>
    {
        public EnemyEventBus enemyEventBus;
        public PlayerEventBus playerEventBus;
        public GameEventBus gameEventBus;

        protected override void Awake()
        {
            base.Awake();
            enemyEventBus = new EnemyEventBus();
            playerEventBus = new PlayerEventBus();
            gameEventBus = new GameEventBus();
        }

    }
}