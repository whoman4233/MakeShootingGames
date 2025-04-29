using Chapter.Base;
using Chapter.ObjectPool;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class GameLobbyState : MonoBehaviour , IGameState
    {
        GameObject parent;
        PlayerBase[] players;
        PlayerBase SelectPlayer;
        SpriteRenderer sp;

        int index;


        public void Enter(GameManager gameManager)
        {
            players = new PlayerBase[7];
            parent = new GameObject("parentObject");
            parent.transform.position = Vector3.zero;

            for (int i = 0; i < 7; i++)
            {
                players[i] = PoolSystemManager.Instance.SpawnPlayer(new Vector3(i * 2, 0, 0));
                players[i].transform.parent = parent.gameObject.transform;
                players[i].SetSelectPlayerPlainStatus("P0" + (i + 1).ToString());
            }

            index = 0;
            ShowSelectPlayer(0);
        }

        void Update()
        {

        }

        public void Exit()
        {
            GameManager.Instance.SetPlayerIndex(index);
        }

        public void ShowSelectPlayer(int PlayerIndex)
        {
            if (index + PlayerIndex < 0 || index + PlayerIndex >= players.Length)
            {
                return;
            }
            else
            {
                index += PlayerIndex;
                SelectPlayer = players[index];
                for (int i = 0; i < players.Length; i++)
                {
                    if (i != index)
                    {
                        sp = players[i].gameObject.GetComponent<SpriteRenderer>();
                        sp.color = Color.gray;
                    }
                    else
                    {
                        sp = players[i].gameObject.GetComponent<SpriteRenderer>();
                        sp.color = Color.white;
                    }
                }

                if (PlayerIndex > 0)
                {
                    parent.transform.position = new Vector3(parent.transform.position.x - 2, parent.transform.position.y, parent.transform.position.z);
                }
                else if (PlayerIndex < 0)
                {
                    parent.transform.position = new Vector3(parent.transform.position.x + 2, parent.transform.position.y, parent.transform.position.z);
                }
            }
        }
    }
}
