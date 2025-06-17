using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;
using UnityEngine.SceneManagement;
using Chapter.Event;
using Chapter.State;

namespace Chapter.Manager
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        private string _targetSceneName;

        private void Start()
        {
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.UIStartButton, OnGameStartButtonClick);
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.UIRegameButton, ReGameButtonClick);
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.UIClearButton, StageClearButtonClick);
        }

        private void OnDestroy()
        {
        }

        public void OnGameStartButtonClick()
        {
            _targetSceneName = "InGameScene";
            SceneManager.LoadScene(_targetSceneName);

        }


        public void ReGameButtonClick()
        {
            _targetSceneName = "LobbyScene 1";
            SceneManager.LoadScene(_targetSceneName);
        }



        public void StageClearButtonClick()
        {
            if (StageManager.Instance.NowStage >= 150)
            {
                Debug.LogError("게임 클리어입니다");
            }
            SceneManager.LoadScene("LobbyScene 1");
        }

    }
}