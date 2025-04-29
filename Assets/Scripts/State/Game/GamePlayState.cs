using Chapter.Base;
using Chapter.Manager;
using Chapter.ObjectPool;
using Chapter.Singleton;
using Chapter.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : IGameState
{
    PlayerBase player;

    public void Enter(GameManager gameManager)
    {
        Debug.Log("GameStart!");

        player = PoolSystemManager.Instance.SpawnPlayer(Vector3.zero);

        player.SetSelectPlayerPlainStatus("P0" + (gameManager._playerIndex + 1).ToString());

        if (player != null)
        {
            Debug.Log($"[Success] Player 오브젝트를 Pool에서 받아옴: {player.name}");
            player.gameObject.SetActive(true); // 혹시라도 false 상태면 강제 활성화
        }
        else
        {
            Debug.LogError("[Error] Pool에서 Player를 가져오는데 실패했습니다.");
        }
    }

    void Update()
    {

    }

    public void Exit()
    {

    }
}
