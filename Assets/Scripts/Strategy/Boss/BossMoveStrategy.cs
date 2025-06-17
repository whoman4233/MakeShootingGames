using Chapter.Manager;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public class BossStayCenterStrategy : IBossMoveStrategy
    {
        public void ExecuteMovement(Transform boss)
        {
            boss.position = new Vector3(0f, 3f, 0f);  // 화면 중앙 고정 위치
        }
    }

    public class BossHorizontalPatrolStrategy : IBossMoveStrategy
    {
        private float speed = 2f;
        private float range = 3f;
        private float startX;

        public void ExecuteMovement(Transform boss)
        {
            if (startX == 0f) startX = boss.position.x;
            boss.position = new Vector3(
                startX + Mathf.PingPong(Time.time * speed, range * 2) - range,
                boss.position.y,
                boss.position.z);
        }
    }

    public class BossDiagonalCrossStrategy : IBossMoveStrategy
    {
        private float speed = 2f;
        public void ExecuteMovement(Transform boss)
        {
            boss.position += new Vector3(Mathf.Sin(Time.time) * Time.deltaTime * speed, -Mathf.Abs(Mathf.Sin(Time.time * 0.5f)) * Time.deltaTime * speed, 0f);
        }
    }

    public class BossRectangularPathStrategy : IBossMoveStrategy
    {
        private readonly Vector3[] corners = {
        new Vector3(-3, 3), new Vector3(3, 3),
        new Vector3(3, 1), new Vector3(-3, 1)
        };
        private int currentCorner = 0;
        private float speed = 2f;

        public void ExecuteMovement(Transform boss)
        {
            Vector3 target = corners[currentCorner];
            boss.position = Vector3.MoveTowards(boss.position, target, Time.deltaTime * speed);

            if (Vector3.Distance(boss.position, target) < 0.1f)
                currentCorner = (currentCorner + 1) % corners.Length;
        }
    }

    public class BossChasePlayerStrategy : IBossMoveStrategy
    {
        private float speed = 2f;

        public void ExecuteMovement(Transform boss)
        {
            var player = PlayerManager.Instance.Player.transform;
            if (player == null) return;

            Vector3 target = new Vector3(player.position.x, boss.position.y, 0);
            boss.position = Vector3.MoveTowards(boss.position, target, Time.deltaTime * speed);
        }
    }

    public class BossThreePointJumpStrategy : IBossMoveStrategy
    {
        private readonly Vector3[] points = {
        new Vector3(-3, 3), new Vector3(0, 2.5f), new Vector3(3, 3)
        };
        private int currentIndex = 0;
        private float speed = 4f;

        public void ExecuteMovement(Transform boss)
        {
            Vector3 target = points[currentIndex];
            boss.position = Vector3.MoveTowards(boss.position, target, Time.deltaTime * speed);

            if (Vector3.Distance(boss.position, target) < 0.1f)
                currentIndex = (currentIndex + 1) % points.Length;
        }
    }

    public class BossBlinkMoveStrategy : IBossMoveStrategy
    {
        private float blinkCooldown = 3f;
        private float lastBlinkTime = -999f;

        public void ExecuteMovement(Transform boss)
        {
            if (Time.time - lastBlinkTime > blinkCooldown)
            {
                Vector3 newPos = new Vector3(Random.Range(-3f, 3f), Random.Range(2f, 4f), 0f);
                boss.position = newPos;
                lastBlinkTime = Time.time;

                // 투명화 연출 가능
                boss.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                boss.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }


    public class BossSpinAroundCenterStrategy : IBossMoveStrategy
    {
        private float radius = 2f;
        private float speed = 1.5f;

        public void ExecuteMovement(Transform boss)
        {
            float angle = Time.time * speed;
            Vector3 center = Vector3.zero;
            boss.position = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
        }
    }

    public class BossPredefinedPathStrategy : IBossMoveStrategy
    {
        public void ExecuteMovement(Transform boss)
        {
            float t = Time.time;
            float x = Mathf.Sin(t * 2f) * 2f;
            float y = 2f + Mathf.Sin(t * 1.5f) * 1f;

            boss.position = new Vector3(x, y, 0);
        }
    }


    public class BossRandomPatternStrategy : IBossMoveStrategy
    {
        private IBossMoveStrategy[] patterns;
        private IBossMoveStrategy current;
        private float changeInterval = 5f;
        private float lastChangeTime = 0f;

        public BossRandomPatternStrategy()
        {
            patterns = new IBossMoveStrategy[]
            {
            new BossHorizontalPatrolStrategy(),
            new BossChasePlayerStrategy(),
            new BossSpinAroundCenterStrategy()
            };
            current = patterns[Random.Range(0, patterns.Length)];
        }

        public void ExecuteMovement(Transform boss)
        {
            if (Time.time - lastChangeTime > changeInterval)
            {
                current = patterns[Random.Range(0, patterns.Length)];
                lastChangeTime = Time.time;
            }

            current.ExecuteMovement(boss);
        }
    }





}