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
                // Laser Ư���� ���� ����
                transform.localScale += Vector3.up * Time.deltaTime * 2f;
                break;

            case BulletType.Charge:
                // ChargeShot Ư���� ���� Ŀ����
                transform.localScale += Vector3.one * Time.deltaTime;
                break;

            case BulletType.Drone:
                // Drone�� Ư���ϰ� �����̰ų� �߰� ����
                break;

            case BulletType.Barrier:
                // Barrier�� �̵� ���� �ֺ��� ����
                break;
        }

        // ȭ�� ������ ������ Ǯ�� ��ȯ
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
               viewportPos.z >= 0; // ȭ�� ���ʿ� �ִ��� Ȯ��
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
