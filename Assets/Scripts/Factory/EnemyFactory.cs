using Chapter.Base;
using Chapter.ObjectPool;
using Chapter.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Manager;
using Chapter.Singleton;

namespace Chapter.Factory
{
    public static class EnemyFactory
    {
        private static List<System.Func<IEnemyMoveStrategy>> moveStrategyGenerators;
        private static List<System.Func<IEnemyAttackStrategy>> attackStrategyGenerators;

        private static GameObject player;

        public static void Init(GameObject playerGameObject)
        {
            player = playerGameObject;
            InitMoveStrategies();
            InitAttackStrategies();
        }

        private static void InitMoveStrategies()
        {

            moveStrategyGenerators = new List<System.Func<IEnemyMoveStrategy>>
            {
                () => new EnemyStraightMoveStrategy(),
                () => new EnemyDiagonalMoveStrategy(),
                () => new EnemyWaveMoveStrategy(),
                () => new EnemyChaseMoveStrategy(player.transform),
                () => new EnemyWaveMoveStrategy(),
                () => new EnemyRandomMoveStrategy(),
                () => new EnemyRotateMoveStrategy(),
                () => new EnemyStayMoveStrategy(),
                () => new EnemyLeapMoveStrategy(),
                () => new EnemyDropMoveStrategy(),
                () => new EnemyAccelerateMoveStrategy(),
                () => new EnemyBlinkMoveStrategy(),
                () => new EnemyPathMoveStrategy(),
                () => new EnemyOrbitMoveStrategy(),
                () => new EnemyLaserChaseMoveStrategy(player.transform)
            };
        }

        private static void InitAttackStrategies()
        {
            attackStrategyGenerators = new List<System.Func<IEnemyAttackStrategy>>
            {

                () => new EnemySingleShotAttackStrategy(),
                () => new EnemyDoubleShotAttackStrategy(),
                () => new EnemyTripleShotAttackStrategy(),
                () => new EnemySpreadShotAttackStrategy(),
                () => new EnemyCircularShotAttackStrategy(),
                () => new EnemyHomingShotAttackStrategy(),
                () => new EnemyRushShotAttackStrategy()
            };
        }

        public static EnemyBase CreateEnemy(string enemyTag, Transform spawnPoint)
        {
            var enemy = PoolSystemManager.Instance.SpawnEnemy(spawnPoint.position);

            if (enemy == null)
            {
                Debug.LogWarning("Enemy 생성 실패");
                return null;
            }

            enemy.SetMoveStrategy(GetRandomMoveStrategy());
            enemy.SetAttackStrategy(GetRandomAttackStrategy());
            enemy.SetSprite(enemySprite());

            return enemy;
        }

        private static IEnemyMoveStrategy GetRandomMoveStrategy()
        {
            int rand = Random.Range(0, moveStrategyGenerators.Count);
            return moveStrategyGenerators[rand]();
        }

        private static IEnemyAttackStrategy GetRandomAttackStrategy()
        {
            int rand = Random.Range(0, attackStrategyGenerators.Count);
            return attackStrategyGenerators[rand]();
        }

        private static Sprite enemySprite()
        {
            int randMax = GameManager.Instance._spriteData.enemySprite.Length;
            int rand = Random.Range(0, randMax);
            return GameManager.Instance._spriteData.enemySprite[rand];
        }

    }
}