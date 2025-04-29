using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    // M01: 직선 이동
    public class EnemyStraightMoveStrategy : IEnemyMoveStrategy
    {
        public void Move(Transform target)
        {
            target.Translate(Vector3.down * Time.deltaTime * 5f);
        }
    }

    // M02: 사선 이동
    public class EnemyDiagonalMoveStrategy : IEnemyMoveStrategy
    {
        public void Move(Transform target)
        {
            Vector3 direction = (Vector3.down + Vector3.right).normalized;
            target.Translate(direction * Time.deltaTime * 5f);
        }
    }

    // M03: 곡선 이동
    public class EnemyCurveMoveStrategy : IEnemyMoveStrategy
    {
        private float time;

        public void Move(Transform target)
        {
            time += Time.deltaTime;
            Vector3 curve = new Vector3(Mathf.Sin(time), -1f, 0f).normalized;
            target.Translate(curve * Time.deltaTime * 3f);
        }
    }

    // M04: 추적 이동
    public class EnemyChaseMoveStrategy : IEnemyMoveStrategy
    {
        private Transform player;

        public EnemyChaseMoveStrategy(Transform playerTarget)
        {
            player = playerTarget;
        }

        public void Move(Transform target)
        {
            Vector3 dir = (player.position - target.position).normalized;
            target.Translate(dir * Time.deltaTime * 5f);
        }
    }

    // M05: 파형 이동
    public class EnemyWaveMoveStrategy : IEnemyMoveStrategy
    {
        private float time;

        public void Move(Transform target)
        {
            time += Time.deltaTime;
            Vector3 wave = new Vector3(Mathf.Sin(time * 5f), -1f, 0f).normalized;
            target.Translate(wave * Time.deltaTime * 4f);
        }
    }

    // M06: 무작위 이동
    public class EnemyRandomMoveStrategy : IEnemyMoveStrategy
    {
        private Vector3 randomDir;
        private float changeInterval = 1.5f;
        private float timer;

        public void Move(Transform target)
        {
            timer += Time.deltaTime;

            if (timer >= changeInterval)
            {
                randomDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), -1f, 0f).normalized;
                timer = 0;
            }

            target.Translate(randomDir * Time.deltaTime * 3f);
        }
    }

    // M07: 회전 이동
    public class EnemyRotateMoveStrategy : IEnemyMoveStrategy
    {
        private float angle = 0;

        public void Move(Transform target)
        {
            angle += Time.deltaTime * 90f;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
            target.position += pos * Time.deltaTime * 2f;
        }
    }

    // M08: 정지 상태
    public class EnemyStayMoveStrategy : IEnemyMoveStrategy
    {
        public void Move(Transform target)
        {
            // 아무것도 하지 않음
        }
    }

    // M09: 도약 이동
    public class EnemyLeapMoveStrategy : IEnemyMoveStrategy
    {
        private float leapInterval = 1f;
        private float timer;

        public void Move(Transform target)
        {
            timer += Time.deltaTime;

            if (timer >= leapInterval)
            {
                target.position += Vector3.down * 2f;
                timer = 0;
            }
        }
    }

    // M10: 낙하 이동
    public class EnemyDropMoveStrategy : IEnemyMoveStrategy
    {
        public void Move(Transform target)
        {
            target.Translate(Vector3.down * Time.deltaTime * 8f);
        }
    }

    // M11: 점점 가속
    public class EnemyAccelerateMoveStrategy : IEnemyMoveStrategy
    {
        private float speed = 2f;

        public void Move(Transform target)
        {
            speed += Time.deltaTime * 2f;
            target.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }

    // M12: 깜빡이 이동
    public class EnemyBlinkMoveStrategy : IEnemyMoveStrategy
    {
        private float blinkInterval = 1f;
        private float timer;

        public void Move(Transform target)
        {
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                target.position += new Vector3(UnityEngine.Random.Range(-2f, 2f), -1f, 0f);
                timer = 0;
            }
        }
    }

    // M13: 패턴 이동
    public class EnemyPathMoveStrategy : IEnemyMoveStrategy
    {
        private Vector3[] path = new Vector3[]
        {
            new Vector3(1f, -1f, 0),
            new Vector3(-1f, -1f, 0),
            new Vector3(0f, -1f, 0)
        };

        private int currentPoint = 0;
        private float speed = 4f;

        public void Move(Transform target)
        {
            if (path.Length == 0) return;

            target.position = Vector3.MoveTowards(target.position, target.position + path[currentPoint], speed * Time.deltaTime);

            if (Vector3.Distance(target.position, target.position + path[currentPoint]) < 0.1f)
            {
                currentPoint = (currentPoint + 1) % path.Length;
            }
        }
    }

    // M14: 원운동
    public class EnemyOrbitMoveStrategy : IEnemyMoveStrategy
    {
        private float angle = 0;
        private float radius = 3f;

        public void Move(Transform target)
        {
            angle += Time.deltaTime * 2f;
            target.position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
        }
    }

    // M15: 레이저 추적 이동
    public class EnemyLaserChaseMoveStrategy : IEnemyMoveStrategy
    {
        private Transform player;

        public EnemyLaserChaseMoveStrategy(Transform playerTarget)
        {
            player = playerTarget;
        }

        public void Move(Transform target)
        {
            Vector3 dir = (player.position - target.position).normalized;
            target.Translate(dir * Time.deltaTime * 7f);
        }
    }
}
