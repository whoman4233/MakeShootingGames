using Chapter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter.ObjectPool
{
    public class EnemyBulletPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyBulletPrefab;
        private BaseObjectPool<EnemyBulletBase> enemybulletPool;

        private void Awake()
        {
            enemybulletPool = new BaseObjectPool<EnemyBulletBase>(enemyBulletPrefab, 5, 20);
        }

        public EnemyBulletBase GetEnemyBullet()
        {
            return enemybulletPool.Get();
        }

        public void ReleaseEnemyBullet(EnemyBulletBase bullet)
        {
            enemybulletPool.Release(bullet);
        }
    }
}
