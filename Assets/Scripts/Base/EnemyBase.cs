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

        private void Awake()
        {
            HP = 3f;
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
            attackStrategy?.Attack(transform);
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
            if(HP <= 0)
            {
                PoolSystemManager.Instance.ReleaseEnemy(this);
            }
        }
    }
}
