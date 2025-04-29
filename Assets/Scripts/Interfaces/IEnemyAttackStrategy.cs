using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public interface IEnemyAttackStrategy
    {
        void Attack(Transform target);
    }
}
