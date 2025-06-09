using Chapter.Base;
using Chapter.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Chapter.Strategy
{
    public class NormalBullet : IBulletStrategy
    {
        private BulletBase bullet;

        public void Initailize(BulletBase thisBullet)
        {
            this.bullet = thisBullet;
            bullet.Initialize(Vector2.up, new Vector2(1,1), 10);
            bullet.OnDisable = true;
        }

        public void OnUpdate()
        {
            bullet.transform.Translate(bullet.direction * bullet.speed * Time.deltaTime);
        }

        public void Trigger(Collider2D collider)
        {
            if(collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyBase>().OnDamage(1);
                PoolSystemManager.Instance.ReleaseBullet(bullet);
            }
        }

    }
    public class AutoAimBullet : IBulletStrategy
    {
        private BulletBase bullet;
        private Transform target;

        public void Initailize(BulletBase thisBullet)
        {
            bullet = thisBullet;
            bullet.OnDisable = true;

            target = GameObject.FindGameObjectWithTag("Enemy")?.transform;
            Vector2 dir = (target != null) ? (target.position - bullet.transform.position).normalized : Vector2.up;
            Debug.Log(dir + "오토에임");
            bullet.Initialize(dir, new Vector2(1, 1), 8f);
        }

        public void OnUpdate()
        {
            bullet.transform.Translate(bullet.direction * bullet.speed * Time.deltaTime);
        }

        public void Trigger(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyBase>()?.OnDamage(1);
                PoolSystemManager.Instance.ReleaseBullet(bullet);
            }
        }
    }

    public class MissileBullet : IBulletStrategy
    {
        private BulletBase bullet;
        private Transform target;

        public void Initailize(BulletBase thisBullet)
        {
            bullet = thisBullet;
            target = GameObject.FindGameObjectWithTag("Enemy")?.transform;
            bullet.Initialize(Vector2.up, new Vector2(1, 1), 6f);
            bullet.OnDisable = true;
        }

        public void OnUpdate()
        {
            if (target != null)
            {
                Vector2 dir = ((Vector2)(target.position - bullet.transform.position)).normalized;
                bullet.direction = Vector2.Lerp(bullet.direction, dir, Time.deltaTime * 2f);
            }

            bullet.transform.Translate(bullet.direction * bullet.speed * Time.deltaTime);
        }

        public void Trigger(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyBase>()?.OnDamage(3);
                PoolSystemManager.Instance.ReleaseBullet(bullet);
            }
        }
    }

    public class LaserBullet : IBulletStrategy
    {
        private BulletBase bullet;
        private float duration = 1.5f;
        private float timer;

        public void Initailize(BulletBase thisBullet)
        {
            bullet = thisBullet;
            bullet.Initialize(Vector2.zero, new Vector2(0.5f, 2f), 0f);
            timer = duration;
            bullet.OnDisable = false;
        }

        public void OnUpdate()
        {
            bullet.transform.localScale += new Vector3(0, Time.deltaTime * 5f, 0);
            bullet.col.size += new Vector2(0, Time.deltaTime * 5f);

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                PoolSystemManager.Instance.ReleaseBullet(bullet);
            }
        }

        public void Trigger(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyBase>()?.OnDamage(1); // 지속적으로 여러 번 피격될 수 있음
            }
        }
    }

    public class CarrierBullet : IBulletStrategy
    {
        private BulletBase bullet;
        private float angle;
        private Transform player;

        public void Initailize(BulletBase thisBullet)
        {
            bullet = thisBullet;
            bullet.Initialize(Vector2.zero, new Vector2(0.5f, 0.5f), 0f);
            angle = Random.Range(0f, 360f);
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            bullet.OnDisable = false;
        }

        public void OnUpdate()
        {
            if (player == null) return;
            angle += Time.deltaTime;
            float radius = 1.5f;
            Vector2 offset = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle)) * radius;
            bullet.transform.position = player.position + (Vector3)offset;
        }

        public void Trigger(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyBase>()?.OnDamage(1);
                PoolSystemManager.Instance.ReleaseBullet(bullet);

            }
        }
    }

    public class BarrierBullet : IBulletStrategy
    {
        private BulletBase bullet;
        private float angle;

        public void Initailize(BulletBase thisBullet)
        {
            bullet = thisBullet;
            bullet.Initialize(Vector2.zero, new Vector2(1f, 1f), 0f);
            angle = Random.Range(0f, 360f);
            bullet.OnDisable = false;
        }

        public void OnUpdate()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player != null)
            {
                angle += Time.deltaTime * 60f;
                float radius = 2.5f;
                bullet.transform.position = player.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            }
        }

        public void Trigger(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyBase>()?.OnDamage(1);
            }
        }
    }

    public class ChargeBullet : IBulletStrategy
    {
        private BulletBase bullet;
        private float chargeTime = 1.0f;
        private float timer;

        public void Initailize(BulletBase thisBullet)
        {
            bullet = thisBullet;
            bullet.Initialize(Vector2.up, new Vector2(0.5f, 0.5f), 5f);
            timer = 0f;
            bullet.OnDisable = true;
        }

        public void OnUpdate()
        {
            timer += Time.deltaTime;

            float scale = Mathf.Lerp(1f, 3f, timer / chargeTime);
            bullet.transform.localScale = Vector3.one * scale;

            bullet.transform.Translate(bullet.direction * bullet.speed * Time.deltaTime);

            if (timer >= chargeTime + 1f)
            {
                PoolSystemManager.Instance.ReleaseBullet(bullet);
            }
        }

        public void Trigger(Collider2D collider)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyBase>()?.OnDamage(5);
                PoolSystemManager.Instance.ReleaseBullet(bullet);
            }
        }
    }
}