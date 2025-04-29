using Chapter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ObjectPool
{
    public class EnemyPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject EnemyPrefab;
        private BaseObjectPool<EnemyBase> enemyPool;

        private void Awake()
        {
            enemyPool = new BaseObjectPool<EnemyBase>(EnemyPrefab, 5, 20);
        }

        public EnemyBase GetEnemy()
        {
            return enemyPool.Get();
        }

        public void ReleaseEnemy(EnemyBase enemy)
        {
            enemyPool.Release(enemy);
        }
    }
}
