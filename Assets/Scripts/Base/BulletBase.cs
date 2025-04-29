using UnityEngine;
using Chapter.ObjectPool;

public enum BulletType
{
    Normal,
    Laser,
    Missile,
    Charge,
    Drone,
    Barrier
}

public class BulletBase : MonoBehaviour, IPoolable
{
    public BulletType bulletType;
    private Vector2 direction;
    private float speed = 10f;

    public void Initialize(Vector2 dir, BulletType type, float customSpeed = 10f)
    {
        direction = dir.normalized;
        bulletType = type;
        speed = customSpeed;
    }

    void Update()
    {
        switch (bulletType)
        {
            case BulletType.Normal:
            case BulletType.Missile:
                transform.Translate(direction * speed * Time.deltaTime);
                break;

            case BulletType.Laser:
                // Laser 특유의 지속 연출
                transform.localScale += Vector3.up * Time.deltaTime * 2f;
                break;

            case BulletType.Charge:
                // ChargeShot 특유의 점점 커지기
                transform.localScale += Vector3.one * Time.deltaTime;
                break;

            case BulletType.Drone:
                // Drone은 특수하게 움직이거나 추가 구현
                break;

            case BulletType.Barrier:
                // Barrier는 이동 없이 주변에 유지
                break;
        }

        // 화면 밖으로 나가면 풀로 반환
        if (!IsVisibleFrom(Camera.main))
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

    public void OnGet()
    {
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        gameObject.SetActive(false);
    }
}
