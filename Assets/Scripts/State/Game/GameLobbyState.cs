using Chapter.Base;
using Chapter.Manager;
using Chapter.ObjectPool;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class GameLobbyState : IGameState
    {
        GameObject parent;
        PlayerBase[] players;
        PlayerBase SelectPlayer;
        SpriteRenderer sp;

        int index;


        public void Enter(GameManager gameManager)
        {
            EventBusManager.Instance.gameEventBus.Publish(Event.GameEventType.Start);
            EventBusManager.Instance.gameEventBus.Subscribe(Event.GameEventType.UIBackButton, BackButton);
            EventBusManager.Instance.gameEventBus.Subscribe(Event.GameEventType.UINextButton, NextButton);

            players = new PlayerBase[7];
            parent = new GameObject("parentObject");
            parent.transform.position = Vector3.zero;

            for (int i = 0; i < 7; i++)
            {
                players[i] = PoolSystemManager.Instance.SpawnPlayer(new Vector3(i * 2, 0, 0));
                players[i].transform.parent = parent.gameObject.transform;
                players[i].SetPlayer(i);
            }

            index = 0;
            ShowSelectPlayer(0);
        }

        void Update()
        {

        }

        public void Exit()
        {
            PlayerManager.Instance.SetPlayerIndex(index);
            EventBusManager.Instance.gameEventBus.Unsubscribe(Event.GameEventType.UIBackButton, BackButton);
            EventBusManager.Instance.gameEventBus.Unsubscribe(Event.GameEventType.UINextButton, NextButton);
        }

        public void NextButton()
        {
            ShowSelectPlayer(1);
        }

        public void BackButton()
        {
            ShowSelectPlayer(-1);
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
