using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public class EnemySingleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("�ܹ�: 1�߾� ���� �ӵ��� �߻�");
        }
    }

    public class EnemyDoubleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("2����: ������ �� �� �߻�");
        }
    }

    public class EnemyTripleShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("3����: �� ���� ������ �߻�");
        }
    }

    public class EnemySpreadShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("��ź: ���� �������� �߻�");
        }
    }

    public class EnemyCircularShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("����ź: 360�� �߻�");
        }
    }

    public class EnemyHomingShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("����ź: �÷��̾� ���� �߻�");
        }
    }

    public class EnemyRushShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("����ź: ��� ���� �߻�");
        }
    }

    public class EnemyExplosiveShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("����ź: ���� �� ���� ������");
        }
    }

    public class EnemySlowShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("���ο�ź: �ǰ� �� �̵� �ӵ� ����");
        }
    }

    public class EnemyLaserAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("������: ������ ���� ������ �߻�");
        }
    }

    public class EnemyDelayedExplosionAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("��������: ���� �ð� �� ����");
        }
    }

    public class EnemyExpandingShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("Ȯ��ź: ���� �����鼭 �߻�");
        }
    }

    public class EnemyLobbedShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("�����: ������ ���� ����");
        }
    }

    public class EnemyRadialShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("���ź: �ֺ� ���� ����");
        }
    }

    public class EnemySuicideAttackAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("����: ���� �÷��̾� ��ó���� ����");
        }
    }

    public class EnemySkillBlockShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("��ų����ź: ��ų ��� �Ұ� ȿ��");
        }
    }

    public class EnemyMapAttackAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("�ʰ���: �ʵ� ��ü�� �����");
        }
    }

    public class EnemySplitShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("�п�ź: ���� ���� �п��ϴ� ź");
        }
    }

    public class EnemyReflectShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("�ݻ�ź: ���� ƨ�� �ݻ�");
        }
    }

    public class EnemyMimicShotAttackStrategy : IEnemyAttackStrategy
    {
        public void Attack(Transform target)
        {
            Debug.Log("�����ϱ�: �÷��̾� ���� ����");
        }
    }
}
