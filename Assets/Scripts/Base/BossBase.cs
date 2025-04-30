using Chapter.ObjectPool;
using Chapter.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Base
{
    public class BossBase : MonoBehaviour, IPoolable
    {
        private IBossAttackStrategy attackStrategy;
        private IBossMoveStrategy moveStrategy;

        SpriteRenderer sp;

        private float HP;
        private float attackSpeed;

        private void Awake()
        {
            HP = 3f;
            attackSpeed = 1;
        }

        public void SetMoveStrategy(IBossMoveStrategy Strategy)
        {
            moveStrategy = Strategy;
        }

        public void SetAttackStrategy(IBossAttackStrategy Strategy)
        {
            attackStrategy = Strategy;
        }

        public void SetSprite(Sprite _bossSprite)
        {
            sp = GetComponent<SpriteRenderer>();
            sp.sprite = _bossSprite;
        }

        private void Update()
        {
            moveStrategy?.ExecuteMovement(transform);
            if (attackStrategy != null && Time.time > attackSpeed)
            {
                attackStrategy?.Attack(transform);
                attackSpeed += Time.time;
            }
        }

        public void OnGet()
        {
            // 풀에서 꺼낼 때 초기화 작업 (ex. 체력 회복, 상태 리셋 등)
            Debug.Log("EnemyBase OnGet 호출");

        }

        public void OnRelease()
        {
            // 풀로 반환될 때 초기화 작업 (ex. 이펙트 끄기, 총알 리셋 등)
            Debug.Log("EnemyBase OnRelease 호출");

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            PoolSystemManager.Instance.ReleaseBullet(collision.gameObject.GetComponent<BulletBase>());
            HP -= 1;
            if (HP <= 0)
            {
                PoolSystemManager.Instance.ReleaseBoss(this);
            }
        }
    }
}
