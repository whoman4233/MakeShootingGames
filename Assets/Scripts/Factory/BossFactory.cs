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
    public static class BossFactory
    {
        private static List<System.Func<IBossMoveStrategy>> moveStrategyGenerators;
        private static List<System.Func<IBossAttackStrategy>> attackStrategyGenerators;

        private static GameObject player;

        public static void Init(GameObject playerGameObject)
        {
            player = playerGameObject;
            InitMoveStrategies();
            InitAttackStrategies();
        }

        private static void InitMoveStrategies()
        {

            moveStrategyGenerators = new List<System.Func<IBossMoveStrategy>>
            {
                () => new BossStayCenterStrategy(),
                () => new BossHorizontalPatrolStrategy(),
                () => new BossDiagonalCrossStrategy(),
                () => new BossRectangularPathStrategy(),
                () => new BossChasePlayerStrategy(),
                () => new BossThreePointJumpStrategy(),
                () => new BossBlinkMoveStrategy(),
                () => new BossSpinAroundCenterStrategy(),
                () => new BossPredefinedPathStrategy(),
                () => new BossRandomPatternStrategy()
            };
        }

        private static void InitAttackStrategies()
        {
            attackStrategyGenerators = new List<System.Func<IBossAttackStrategy>>
            {

                () => new BossAttackCircularRapidFireStrategy(),
                () => new BossAttackHomingMissileStrategy(),
                () => new BossAttackSpinningSpreadStrategy(),
                () => new BossAttackCircularLaserStrategy(),
                () => new BossAttackDelayedExplosionMissileStrategy(),
                () => new BossAttackThreeWayLaserStrategy(),
                () => new BossAttackCrossPatternStrategy(),
                () => new BossAttackMixedPatternStrategy(),
                () => new BossAttackChainBulletStrategy(),
                () => new BossAttackHeavyBulletStrategy(),
                () => new BossAttackReflectBulletStrategy(),
                () => new BossAttackBladeThrowStrategy(),
                () => new BossAttackAccelFireStrategy(),
                () => new BossAttackTrapBombStrategy()
            };
        }

        public static BossBase CreateBoss(string enemyTag, Transform spawnPoint)
        {
            var boss = PoolSystemManager.Instance.SpawnBoss(spawnPoint.position);

            if (boss == null)
            {
                Debug.LogWarning("Boss 생성 실패");
                return null;
            }

            boss.SetMoveStrategy(GetRandomMoveStrategy());
            boss.SetAttackStrategy(GetRandomAttackStrategy());
            boss.SetSprite(bossSprite());

            return boss;
        }

        private static IBossMoveStrategy GetRandomMoveStrategy()
        {
            int rand = Random.Range(0, moveStrategyGenerators.Count - 1);
            return moveStrategyGenerators[rand]();
        }

        private static IBossAttackStrategy GetRandomAttackStrategy()
        {
            int rand = Random.Range(0, attackStrategyGenerators.Count - 1);
            return attackStrategyGenerators[rand]();
        }

        private static Sprite bossSprite()
        {
            int randMax = GameManager.Instance._spriteData.enemySprite.Length;
            int rand = Random.Range(0, randMax);
            return GameManager.Instance._spriteData.enemySprite[rand];
        }

    }
}