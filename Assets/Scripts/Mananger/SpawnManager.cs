using Chapter.Base;
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
        [Header("���� ������ ����Ʈ")]
        public List<Transform> spawnPoints;

        [Header("�� ���� �ֱ� (��)")]
        public float enemySpawnInterval = 2f;

        private float enemySpawnTimer;
        GameObject playerGameObject;

        IEnumerator Start()
        {
            yield return null;

            playerGameObject = GameManager.Instance._playerGameObject;
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
                Debug.LogWarning("Spawn Points�� �����ϴ�");
                return;
            }

            // ���� ���� ��ġ ����
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            var enemy = EnemyFactory.CreateEnemy("Enemy", spawnPoint);
            if (enemy != null)
            {
                enemy.transform.position = spawnPoint.position;
            }
            else
            {
                Debug.LogError("�� ���� ���� �߻�");
            }
        }

    }

}