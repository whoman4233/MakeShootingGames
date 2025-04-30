using Chapter.Base;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ObjectPool
{
    public class PoolSystemManager : Singleton<PoolSystemManager>
    {
        public PlayerPoolManager playerPoolManager;
        public EnemyPoolManager enemyPoolManager;
        public BulletPoolManager bulletPoolManager;
        public EnemyBulletPoolManager enemyBulletPoolManager;
        public BossPoolManager bossPoolManager;
        // 여기에 EnemyPoolManager, BulletPoolManager 등 추가


        public PlayerBase SpawnPlayer(Vector3 position)
        {
            var player = playerPoolManager.GetPlayer();
            player.transform.position = position;
            return player;
        }

        public void ReleasePlayer(PlayerBase player)
        {
            playerPoolManager.ReleasePlayer(player);
        }

        public EnemyBase SpawnEnemy(Vector3 position)
        {
            var enemy = enemyPoolManager.GetEnemy();
            enemy.transform.position = position;
            return enemy;
        }

        public void ReleaseEnemy(EnemyBase enemy)
        {
            enemyPoolManager.ReleaseEnemy(enemy);
        }

        public T SpawnBullet<T>(Vector3 position) where T : BulletBase
        {
            BulletBase bulletBase = SpawnBullet(position);
            return bulletBase as T;
        }

        public BulletBase SpawnBullet(Vector3 position)
        {
            var bullet = bulletPoolManager.GetBullet();
            bullet.transform.position = position;
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        public void ReleaseBullet(BulletBase bullet)
        {
            bulletPoolManager.ReleaseBullet(bullet);
        }

        public EnemyBulletBase SpawnEnemyBullet(Vector3 position)
        {
            var bullet = enemyBulletPoolManager.GetEnemyBullet();
            bullet.transform.position = position;
            return bullet;
        }

        public void ReleaseEnemyBullet(EnemyBulletBase bullet)
        {
            enemyBulletPoolManager.ReleaseEnemyBullet(bullet);
        }

        public BossBase SpawnBoss(Vector3 position)
        {
            var boss = bossPoolManager.GetBoss();
            boss.transform.position = position;
            return boss;
        }

        public void ReleaseBoss(BossBase boss)
        {
            bossPoolManager.ReleaseBoss(boss);
        }


    }
}