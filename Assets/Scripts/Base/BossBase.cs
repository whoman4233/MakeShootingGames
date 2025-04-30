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
            // Ǯ���� ���� �� �ʱ�ȭ �۾� (ex. ü�� ȸ��, ���� ���� ��)
            Debug.Log("EnemyBase OnGet ȣ��");

        }

        public void OnRelease()
        {
            // Ǯ�� ��ȯ�� �� �ʱ�ȭ �۾� (ex. ����Ʈ ����, �Ѿ� ���� ��)
            Debug.Log("EnemyBase OnRelease ȣ��");

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
