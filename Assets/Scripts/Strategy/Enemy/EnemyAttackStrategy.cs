using UnityEngine;
using Chapter.ObjectPool;
using Chapter.Singleton;
using Chapter.Manager;

namespace Chapter.Strategy
{

    public class EnemySingleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            bullet.Initialize(Vector2.down, 5f);
        }
    }

    public class EnemyDoubleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize((Vector2.down + Vector2.right * 0.2f * i).normalized, 6f);
            }
        }
    }

    public class EnemyTripleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = -1; i <= 1; i++)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize((Vector2.down + Vector2.right * 0.3f * i).normalized, 6f);
            }
        }
    }

    public class EnemySpreadShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            for (int i = -2; i <= 2; i++)
            {
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize((Vector2.down + Vector2.right * 0.4f * i).normalized, 5f);
            }
        }
    }

    public class EnemyCircularShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            int bulletCount = 12;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = 360f / bulletCount * i;
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
                bullet.Initialize(dir, 4f);
            }
        }
    }

    public class EnemyHomingShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            if (PlayerManager.Instance.Player.transform == null) return;

            Vector2 dir = (PlayerManager.Instance.Player.transform.position - shooter.position).normalized;
            var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            bullet.Initialize(dir, 6f);
        }
    }

    public class EnemyRushShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform shooter)
        {
            var bullet = PoolSystemManager.Instance.SpawnEnemyBullet(shooter.position);
            bullet.Initialize(Vector2.down, 10f);
        }
    }
}
