using Chapter.Base;
using Chapter.Event;
using Chapter.Manager;
using Chapter.ObjectPool;
using Chapter.Singleton;
using Chapter.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class GamePlayState : IGameState
    {
        PlayerBase player;
        int EnemyDeadCounter;

        public void Enter(GameManager gameManager)
        {
            Debug.Log("GameStart!");

            player = PoolSystemManager.Instance.SpawnPlayer(Vector3.zero);
            PlayerManager.Instance.InitializePlayer(player);

            EnemyDeadCounter = 0;

            EventBusManager.Instance.enemyEventBus.Subscribe(EnemyEventType.Dead, OnEnemyDead);

            if (player != null)
            {
                Debug.Log($"[Success] Player 오브젝트를 Pool에서 받아옴: {player.name}");
                PlayerManager.Instance.Player.SetActive(true);
            }
            else
            {
                Debug.LogError("[Error] Pool에서 Player를 가져오는데 실패했습니다.");
            }

            EnemyDeadCounter = 0;
        }


        public void OnEnemyDead()
        {
            EnemyDeadCounter++;
            Debug.Log(EnemyDeadCounter + "명의 적 사망");
            if (EnemyDeadCounter >= 1)
            {
                EnemyDeadCounter = 0;
                EventBusManager.Instance.enemyEventBus.Publish(EnemyEventType.SpawnBoss);
            }
        }

        void Update()
        {

        }

        public void Exit()
        {
            EventBusManager.Instance.enemyEventBus.Unsubscribe(EnemyEventType.Dead, OnEnemyDead);
        }
    }

}