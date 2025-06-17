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
        public SpriteData _spriteData;

        IGameState currentState;

        private IGameState
            _gameLobbyState, _gamePlayState;


        public void Start()
        {
            _gameLobbyState = new GameLobbyState();
            _gamePlayState = new GamePlayState();

            EventBusManager.Instance.gameEventBus.Publish(GameEventType.Start);
            SceneManager.sceneLoaded += OnSceneLoaded;

            ChangeState(_gameLobbyState);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void ChangeState(IGameState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter(this);
        }

        private void Update()
        {
            currentState?.Update();

        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "InGameScene")
            {
                ChangeState(_gamePlayState);
            }

            if (scene.name == "LobbyScene 1")
            {
                ChangeState(_gameLobbyState);
                EventBusManager.Instance.gameEventBus.Publish(GameEventType.Start);
            }
        }
    }
}