using Chapter.Base;
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

        public void Enter(GameManager gameManager)
        {
            Debug.Log("GameStart!");

            GameManager.Instance.Initialize();

            player = PoolSystemManager.Instance.SpawnPlayer(Vector3.zero);

            if (player != null)
            {
                Debug.Log($"[Success] Player 오브젝트를 Pool에서 받아옴: {player.name}");
                player.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("[Error] Pool에서 Player를 가져오는데 실패했습니다.");
            }

            //EnemyDeadCounter Init
            PlayerManager.Instance.InitializePlayer();

        }

        void Update()
        {

        }

        public void Exit()
        {

        }
    }

}