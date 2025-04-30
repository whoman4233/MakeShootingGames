using UnityEngine;
using Chapter.ObjectPool;

namespace Chapter.Base
{
    public class EnemyBulletBase : MonoBehaviour, IPoolable
    {
        private Vector2 direction;
        private float speed;

        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);

            // ȭ�� ������ ������ Ǯ�� ��ȯ
            if (!IsVisibleFrom(Camera.main))
            {
                PoolSystemManager.Instance.ReleaseEnemyBullet(this);
            }
        }

        public void Initialize(Vector2 dir, float spd)
        {
            direction = dir.normalized;
            speed = spd;
        }

        private bool IsVisibleFrom(Camera cam)
        {
            Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
            return viewportPos.x >= 0 && viewportPos.x <= 1 &&
                   viewportPos.y >= 0 && viewportPos.y <= 1 &&
                   viewportPos.z >= 0;
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // �÷��̾�� ������ �ִ� ó��
                // other.GetComponent<PlayerBase>()?.TakeDamage(1);  // ����
                PoolSystemManager.Instance.ReleaseEnemyBullet(this);
            }
        }
    }
}
