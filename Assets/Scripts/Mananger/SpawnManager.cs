using Chapter.Base;
using Chapter.Event;
using Chapter.Factory;
using Chapter.ObjectPool;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("스폰 포지션 리스트")]
        public List<Transform> spawnPoints;

        [Header("적 스폰 주기 (초)")]
        public float enemySpawnInterval = 2f;

        private float enemySpawnTimer;
        GameObject playerGameObject;


        IEnumerator Start()
        {
            yield return null;

            playerGameObject = PlayerManager.Instance.Player;
            EventBusManager.Instance.enemyEventBus.Subscribe(EnemyEventType.SpawnBoss, SpawnBoss);
            EnemyFactory.Init(playerGameObject);
        }

        private void Update()
        {
            enemySpawnTimer += Time.deltaTime;

            if (enemySpawnTimer >= enemySpawnInterval)
            {
                SpawnEnemy();
                enemySpawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning("Spawn Points가 없습니다");
                return;
            }

            // 랜덤 스폰 위치 선택
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count - 1)];

            var enemy = EnemyFactory.CreateEnemy("Enemy", spawnPoint);
            if (enemy != null)
            {
                enemy.transform.position = spawnPoint.position;
            }
            else
            {
                Debug.LogError("적 생성 문제 발생");
            }
        }

        public void SpawnBoss()
        {
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning("Spawn Points가 없습니다");
                return;
            }

            // 랜덤 스폰 위치 선택
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            BossFactory.Init(PlayerManager.Instance.Player);
            var boss = BossFactory.CreateBoss("Boss", spawnPoint);
            if (boss != null)
            {
                boss.transform.position = spawnPoint.position;
            }
            else
            {
                Debug.LogError("적 생성 문제 발생");
            }
        }

    }

}