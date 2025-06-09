using Chapter.Base;
using Chapter.Factory;
using Chapter.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public abstract class PlayerAttackStrategy : IWeaponStrategy
    {
        protected float bulletSpeed = 10f;
        protected float damage = 1f;

        public abstract void Shoot(Transform firePoint);

        /// <summary>
        /// BulletBase를 생성하고 초기화하는 메서드
        /// </summary>
        protected void FireBullet(Transform firePoint, Vector2 direction, IBulletStrategy bulletStrategy)
        {
            var bullet = PoolSystemManager.Instance.SpawnBullet<BulletBase>(firePoint.position);
            if (bullet != null)
            {
                bullet.SetBehavior(bulletStrategy);
                bulletStrategy.Initailize(bullet);
            }
            else
            {
                Debug.LogError($"{bulletStrategy} 탄환 생성 실패!");
            }
        }
    }

    // ─────────────────────────────────────

    public class AssaultRifle : PlayerAttackStrategy
    {
        public AssaultRifle()
        {
            bulletSpeed = 15f;
            damage = 5f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.Normal));
        }
    }

    public class AutoAim : PlayerAttackStrategy
    {
        public AutoAim()
        {
            bulletSpeed = 12f;
            damage = 4f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.AutoAim));
        }
    }

    public class MissileLauncher : PlayerAttackStrategy
    {
        public MissileLauncher()
        {
            bulletSpeed = 7f;
            damage = 15f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.Missile));
        }
    }

    public class LaserBeam : PlayerAttackStrategy
    {
        public LaserBeam()
        {
            bulletSpeed = 20f;
            damage = 10f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.Laser));
        }
    }

    public class DroneCarrier : PlayerAttackStrategy
    {
        public DroneCarrier()
        {
            bulletSpeed = 0f;
            damage = 0f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.Drone));
        }
    }

    public class EnergyBarrier : PlayerAttackStrategy
    {
        public EnergyBarrier()
        {
            bulletSpeed = 0f;
            damage = 0f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.Barrier));
        }
    }

    public class ChargeShot : PlayerAttackStrategy
    {
        public ChargeShot()
        {
            bulletSpeed = 5f;
            damage = 20f;
        }

        public override void Shoot(Transform firePoint)
        {
            FireBullet(firePoint, Vector2.up, BulletStrategyFactory.Create(BulletType.Charge));
        }
    }
}
