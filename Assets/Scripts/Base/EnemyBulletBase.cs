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

            // 화면 밖으로 나가면 풀로 반환
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
                // 플레이어에게 데미지 주는 처리
                // other.GetComponent<PlayerBase>()?.TakeDamage(1);  // 예시
                PoolSystemManager.Instance.ReleaseEnemyBullet(this);
            }
        }
    }
}
