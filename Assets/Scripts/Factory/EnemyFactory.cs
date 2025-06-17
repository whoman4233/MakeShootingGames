using Chapter.Base;
using Chapter.ObjectPool;
using Chapter.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Manager;
using Chapter.Singleton;
using Chapter.Data;

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

            int rand = Random.Range(0, 39);
            string[] patterns = GetEnemyStatus(rand + 1);

            int M = int.Parse(patterns[0]) - 1;
            int A = int.Parse(patterns[1]) - 1;

            enemy.SetAttackStrategy(GetAttackStrategy(A));
            enemy.SetMoveStrategy(GetMoveStategy(M));

            enemy.SetSprite(enemySprite());

            return enemy;
        }


        private static IEnemyAttackStrategy GetAttackStrategy(int A)
        {
            if (A >= 0 && A < attackStrategyGenerators.Count)
            {
                return attackStrategyGenerators[A]();
            }
            else
            {
                Debug.LogError($"[EnemyFactory] 전략 인덱스 {A}가 유효하지 않습니다.");
                return attackStrategyGenerators[0]();
            }
        }

        private static IEnemyMoveStrategy GetMoveStategy(int M)
        {
            if(M>=0 && M < moveStrategyGenerators.Count)
            {
                return moveStrategyGenerators[M]();
            }
            else
            {
                Debug.LogError($"[EnemyFactory] 전략 인덱스 {M}가 유효하지 않습니다.");
                return moveStrategyGenerators[0]();
            }
        }

        private static string[] GetEnemyStatus(int rand)
        {
            string moveStrategy;
            string attackStrategy;

            if(rand < 10)
            {
                attackStrategy = DataManager.Instance.GetEnemyStatus("E0" + rand.ToString()).AttackPattern;
                moveStrategy = DataManager.Instance.GetEnemyStatus("E0" + rand.ToString()).MovePattern1;
            }
            else
            {
                attackStrategy = DataManager.Instance.GetEnemyStatus("E" + rand.ToString()).AttackPattern;
                moveStrategy = DataManager.Instance.GetEnemyStatus("E" + rand.ToString()).MovePattern1;
            }

            attackStrategy = attackStrategy.Substring(1);
            moveStrategy = moveStrategy.Substring(1);

            return new string[] { moveStrategy, attackStrategy };
        }

        private static Sprite enemySprite()
        {
            int randMax = GameManager.Instance._spriteData.enemySprite.Length;
            int rand = Random.Range(0, randMax);
            return GameManager.Instance._spriteData.enemySprite[rand];
        }

    }
}