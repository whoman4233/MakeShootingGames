using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using Chapter.ObjectPool;
using Chapter.Singleton;

namespace Chapter.Strategy
{
    public class BossAttackCircularRapidFireStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            int bulletCount = 24;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = 360f / bulletCount * i;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize(dir, 7f);
            }
        }
    }

    public class BossAttackHomingMissileStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = 0; i < 3; i++)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                Vector2 dir = (GameManager.Instance._playerGameObject.transform.position - shooter.position).normalized;
                bullet.Initialize(dir, 5f);
            }
        }
    }

    public class BossAttackSpinningSpreadStrategy : IBossAttackStrategy
    {
        private float angleOffset = 0;

        public void Attack(Transform shooter)
        {
            int bulletCount = 12;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = angleOffset + 360f / bulletCount * i;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize(dir, 5f);
            }
            angleOffset += 10f;
        }
    }

    public class BossAttackCircularLaserStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var laser = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            laser.Initialize(Vector2.down, 20f);
        }
    }

    public class BossAttackDelayedExplosionMissileStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            bullet.Initialize(Vector2.down, 3f);
        }
    }

    public class BossAttackThreeWayLaserStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = -1; i <= 1; i++)
            {
                var laser = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                Vector2 dir = (Vector2.down + Vector2.right * i * 0.5f).normalized;
                laser.Initialize(dir, 15f);
            }
        }
    }

    public class BossAttackCrossPatternStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            Vector2[] directions = new Vector2[]
            {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right
            };
            foreach (var dir in directions)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize(dir, 6f);
            }
        }
    }

    public class BossAttackMixedPatternStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            // 원형 공격
            int bulletCount = 8;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = 360f / bulletCount * i;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize(dir, 5f);
            }
            // 사선 공격
            for (int i = -1; i <= 1; i += 2)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize((Vector2.down + Vector2.right * i).normalized, 6f);
            }
        }
    }

    public class BossAttackChainBulletStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = 0; i < 12; i++)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                Vector2 dir = Quaternion.Euler(0, 0, 30 * i) * Vector2.down;
                bullet.Initialize(dir, 4f);
            }
        }
    }

    public class BossAttackHeavyBulletStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            bullet.Initialize(Vector2.down, 2f);
        }
    }

    public class BossAttackReflectBulletStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            bullet.Initialize(Vector2.down, 5f);
        }
    }

    public class BossAttackBladeThrowStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var blade = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            blade.Initialize(Vector2.down, 7f);
        }
    }

    public class BossAttackAccelFireStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = 0; i < 5; i++)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize(Vector2.down, 4f + i);
            }
        }
    }

    public class BossAttackTrapBombStrategy : IBossAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = 0; i < 3; i++)
            {
                var trap = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position + Vector3.down * i);
                trap.Initialize(Vector2.zero, 0f); // 고정형 트랩
            }
        }
    }
}
