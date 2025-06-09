using Chapter.Event;
using Chapter.ObjectPool;
using Chapter.Strategy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Base
{
    public class EnemyBase : MonoBehaviour, IPoolable
    {
        private IEnemyAttackStrategy attackStrategy;
        private IEnemyMoveStrategy moveStrategy;

        SpriteRenderer sp;

        private float HP;
        private float attackSpeed;

        private void Awake()
        {
            HP = 3f;
            attackSpeed = 1;
        }

        public void SetMoveStrategy(IEnemyMoveStrategy Strategy)
        {
            moveStrategy = Strategy;
        }

        public void SetAttackStrategy(IEnemyAttackStrategy Strategy)
        {
            attackStrategy = Strategy;
        }

        public void SetSprite(Sprite _enemySprite)
        {
            sp = GetComponent<SpriteRenderer>();
            sp.sprite = _enemySprite;
        }

        private void Update()
        {
            moveStrategy?.Move(transform);
            if (attackStrategy != null && Time.time > attackSpeed)
            {
                attackStrategy?.Attack(transform);
                attackSpeed += Time.time;
                EnemyEventBus enemyEvent = new EnemyEventBus();
                enemyEvent.Publish(EnemyEventType.Dead);
            }
        }

        public void OnGet()
        {
            // Ǯ���� ���� �� �ʱ�ȭ �۾� (ex. ü�� ȸ��, ���� ���� ��)

        }

        public void OnRelease()
        {
            // Ǯ�� ��ȯ�� �� �ʱ�ȭ �۾� (ex. ����Ʈ ����, �Ѿ� ���� ��)

        }


        public void OnDamage(int Damage)
        {
            HP -= Damage;
            if (HP <= 0)
            {
                PoolSystemManager.Instance.ReleaseEnemy(this);
            }
        }
    }
}
