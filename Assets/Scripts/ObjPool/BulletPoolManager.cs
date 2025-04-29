using Chapter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter.ObjectPool
{
    public class BulletPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        private BaseObjectPool<BulletBase> bulletPool;

        private void Awake()
        {
            bulletPool = new BaseObjectPool<BulletBase>(bulletPrefab, 5, 20);
        }

        public BulletBase GetBullet()
        {
            return bulletPool.Get();
        }

        public void ReleaseBullet(BulletBase bullet)
        {
            bulletPool.Release(bullet);
        }
    }
}
