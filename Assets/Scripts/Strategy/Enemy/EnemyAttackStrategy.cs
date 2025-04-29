using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public class EnemySingleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("단발: 1발씩 느린 속도로 발사");
        }
    }

    public class EnemyDoubleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("2연사: 빠르게 두 발 발사");
        }
    }

    public class EnemyTripleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("3연사: 세 발을 빠르게 발사");
        }
    }

    public class EnemySpreadShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("산탄: 여러 방향으로 발사");
        }
    }

    public class EnemyCircularShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("원형탄: 360도 발사");
        }
    }

    public class EnemyHomingShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("추적탄: 플레이어 추적 발사");
        }
    }

    public class EnemyRushShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("돌진탄: 고속 직선 발사");
        }
    }

    public class EnemyExplosiveShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("폭발탄: 명중 시 범위 데미지");
        }
    }

    public class EnemySlowShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("슬로우탄: 피격 시 이동 속도 감소");
        }
    }

    public class EnemyLaserAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("레이저: 강력한 직선 레이저 발사");
        }
    }

    public class EnemyDelayedExplosionAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("지연폭발: 일정 시간 후 폭발");
        }
    }

    public class EnemyExpandingShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("확산탄: 점점 퍼지면서 발사");
        }
    }

    public class EnemyLobbedShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("곡사포: 포물선 궤적 공격");
        }
    }

    public class EnemyRadialShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("방사탄: 주변 범위 공격");
        }
    }

    public class EnemySuicideAttackAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("자폭: 적이 플레이어 근처에서 폭발");
        }
    }

    public class EnemySkillBlockShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("스킬봉쇄탄: 스킬 사용 불가 효과");
        }
    }

    public class EnemyMapAttackAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("맵공격: 필드 전체에 디버프");
        }
    }

    public class EnemySplitShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("분열탄: 여러 개로 분열하는 탄");
        }
    }

    public class EnemyReflectShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("반사탄: 벽에 튕겨 반사");
        }
    }

    public class EnemyMimicShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("따라하기: 플레이어 공격 복제");
        }
    }
}
