using Chapter.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Factory
{
    public static class BulletStrategyFactory
    {
        public static IBulletStrategy Create(BulletType type)
        {
            return type switch
            {
                BulletType.Normal => new NormalBullet(),
                BulletType.AutoAim => new AutoAimBullet(),
                BulletType.Missile => new MissileBullet(),
                BulletType.Laser => new LaserBullet(),
                BulletType.Charge => new ChargeBullet(),
                BulletType.Drone => new CarrierBullet(),
                BulletType.Barrier => new BarrierBullet(),
                _ => throw new System.NotImplementedException($"BulletType {type} ¹Ì±¸Çö")
            };
        }
    }
}