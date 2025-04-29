using Chapter.Base;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ObjectPool
{
    public class PlayerPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        private BaseObjectPool<PlayerBase> playerPool;

        private void Awake()
        {
            playerPool = new BaseObjectPool<PlayerBase>(playerPrefab, 5, 20);
        }

        public PlayerBase GetPlayer()
        {
            return playerPool.Get();
        }

        public void ReleasePlayer(PlayerBase player)
        {
            playerPool.Release(player);
        }
    }
}
