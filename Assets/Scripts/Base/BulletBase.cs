using UnityEngine;
using Chapter.ObjectPool;
using Chapter.Strategy;


namespace Chapter.Factory
{
    public enum BulletType
    {
        Normal, AutoAim, Missile, Laser, Charge, Drone, Barrier
    }
}

namespace Chapter.Base
{
    public class BulletBase : MonoBehaviour, IPoolable
    {
        public IBulletStrategy strategy;
        public Vector2 direction;
        public float speed = 10f;
        public bool OnDisable;

        private bool isReleased = false;

 
        public BoxCollider2D col;

        public void SetBehavior(IBulletStrategy newStrategy)
        {
            strategy = newStrategy;
        }

        public void Initialize(Vector2 dir, Vector2 size, float customSpeed)
        {
            direction = dir.normalized;
            speed = customSpeed;
            col = GetComponent<BoxCollider2D>();
            col.size = size;
            isReleased = false;
        }

        void Update()
        {
            strategy?.OnUpdate();

            // 화면 밖으로 나가면 풀로 반환
            if (!IsVisibleFrom(Camera.main) && OnDisable)
            {
                PoolSystemManager.Instance.ReleaseBullet(this);
            }

        }

        private bool IsVisibleFrom(Camera cam)
        {
            Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
            return viewportPos.x >= 0 && viewportPos.x <= 1 &&
                   viewportPos.y >= 0 && viewportPos.y <= 1 &&
                   viewportPos.z >= 0; // 화면 앞쪽에 있는지 확인
        }

        public void Release()
        {
            if (isReleased) return;

            isReleased = true;
            PoolSystemManager.Instance.ReleaseBullet(this);
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {
            isReleased = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            strategy?.Trigger(collision);
        }
    }
}