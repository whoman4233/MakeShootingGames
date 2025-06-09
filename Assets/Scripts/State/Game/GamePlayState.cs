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

            player.SetSelectPlayerPlainStatus("P0" + (gameManager._playerIndex + 1).ToString());

            if (player != null)
            {
                Debug.Log($"[Success] Player ������Ʈ�� Pool���� �޾ƿ�: {player.name}");
                player.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("[Error] Pool���� Player�� �������µ� �����߽��ϴ�.");
            }
        }

        void Update()
        {

        }

        public void Exit()
        {

        }
    }

}