using Chapter.CharacterBase;
using Chapter.Data;
using Chapter.Event;
using Chapter.Manager;
using Chapter.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton
{
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        public GameObject _playerGameObject;
        public int _playerIndex;
        public SpriteData _spriteData;

        IGameState currentState;

        private IGameState
            _gameLobbyState, _gamePlayState;

        UIManager uiManager;

        int EnemyDeadCounter;
        int PlayerHP;
        public int NowStage = 1;


        public void Start()
        {
            _gameLobbyState = new GameLobbyState();
            _gamePlayState = new GamePlayState();

            EventBusManager.Instance.enemyEventBus.Subscribe(EnemyEventType.Dead, OnEnemyDead); //SpawnManager 또는 EnemyManager에 분산
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.End, Clear); //의미없이 쓰이긴했음 수정필요
            EventBusManager.Instance.playerEventBus.Subscribe(PlayerEventType.OnHit, OnPlayerHit); //이거도 플레이어측에서 할수있도록 변경
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.Start);

           _sessionStartTime = DateTime.Now;
            Debug.Log(
                "Game session start @: " + DateTime.Now);

            NowStage = 100;
            ChangeState(new GameLobbyState());
            uiManager = FindObjectOfType<UIManager>();
            uiManager.LobbyInit();
            Debug.Log(NowStage + "현재 스테이지");

        }

        void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;

            TimeSpan timeDifference =
                _sessionEndTime.Subtract(_sessionStartTime);

            Debug.Log(
                "Game session ended @: " + DateTime.Now);
            Debug.Log(
                "Game session lasted: " + timeDifference);
        }


        public void ChangeState(IGameState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter(this);
        }

        public void OnNextButtonClick()
        {
            if (currentState is GameLobbyState lobbyState)
            {
                lobbyState.ShowSelectPlayer(1);
            }
        }

        public void OnBackButtonClick()
        {
            if (currentState is GameLobbyState lobbyState)
            {
                lobbyState.ShowSelectPlayer(-1);
            }
        }

        public void SetPlayerIndex(int index)
        {
            _playerIndex = index;
        }

        public void OnGameStartButtonClick()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("InGameScene");

        }

        public void ReGameButtonClick()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("LobbyScene 1");
        }

        public void StageClearButtonClick()
        {
            NowStage++;
            if (NowStage >= 150)
            {
                Debug.LogError("게임 클리어입니다");
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("LobbyScene 1");
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "InGameScene")
            {
                ChangeState(_gamePlayState);
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }

            if(scene.name == "LobbyScene 1")
            {
                ChangeState(_gameLobbyState);
                uiManager = FindObjectOfType<UIManager>();
                uiManager.LobbyInit();
                EventBusManager.Instance.gameEventBus.Publish(GameEventType.Start);
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }

        public void OnEnemyDead()
        {
            EnemyDeadCounter++;
            Debug.Log(EnemyDeadCounter + "명의 적 사망");
            if(EnemyDeadCounter >= 1)
            {
                EnemyDeadCounter = 0;
                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
                spawnManager.SpawnBoss();
            }
        }

        public void OnPlayerHit()
        {
            if(currentState is GamePlayState playState)
            {
                PlayerHP -= 1;
                uiManager.UpdateHp(PlayerHP);
                if(PlayerHP <= 0)
                {
                    uiManager.UpdateHp(PlayerHP);
                    EventBusManager.Instance.playerEventBus.Publish(PlayerEventType.OnDead);
                }
            }
        }

        public void Initialize()
        {
            if(currentState is GamePlayState gamePlayState)
            {
                EnemyDeadCounter = 0;
                PlayerHP = 3;
                uiManager = FindObjectOfType<UIManager>();
                uiManager.Initalize();
                uiManager.SetMaxHp(PlayerHP);
                
            }
        }

        public void Clear()
        {
            Debug.Log("실행됨");
        }


        private void Update()
        {
            currentState?.Update();

        }


    }
}