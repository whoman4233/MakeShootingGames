using Chapter.CharacterBase;
using Chapter.Data;
using Chapter.Event;
using Chapter.State;
using System;
using System.Collections;
using System.Collections.Generic;
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
            

        public void Start()
        {
            _gameLobbyState = new GameLobbyState();
            _gamePlayState = new GamePlayState();

            _sessionStartTime = DateTime.Now;
            Debug.Log(
                "Game session start @: " + DateTime.Now);

            ChangeState(new GameLobbyState());

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

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "InGameScene")
            {
                ChangeState(_gamePlayState);
                SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 제거 (중복 방지)
            }
        }

        


        private void Update()
        {
            currentState?.Update();

        }


    }
}