using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;
using Chapter.Event;

namespace Chapter.Manager
{
    public class StageManager : Singleton<StageManager>
    {
        public int NowStage { get; private set; }

        private void Start()
        {
            NowStage = 1;
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.UIClearButton, AdvanceStage);
        }

        public void AdvanceStage()
        {
            NowStage++;
        }


    }
}