using Chapter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Chapter.ObjectPool
{
    public class BossPoolManager : MonoBehaviour
    {
        [SerializeField] private GameObject bossPrefab;
        private BaseObjectPool<BossBase> bossPool;

        private void Awake()
        {
            bossPool = new BaseObjectPool<BossBase>(bossPrefab, 5, 20);
        }

        public BossBase GetBoss()
        {
            return bossPool.Get();
        }

        public void ReleaseBoss(BossBase bullet)
        {
            bossPool.Release(bullet);
        }
    }
}
