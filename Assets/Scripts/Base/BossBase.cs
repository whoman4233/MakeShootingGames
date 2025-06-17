using Chapter.Event;
using Chapter.Manager;
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

        GameEventBus gameEventBus;

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

        }

        public void OnRelease()
        {
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.End);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out BulletBase bullet))
            {
                collision.gameObject.GetComponent<BulletBase>().Release();
                HP -= 1;
                if (HP <= 0)
                {
                    PoolSystemManager.Instance.ReleaseBoss(this);
                }
            }
        }
    }
}
