using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public interface IEnemyMoveStrategy
    {
        void Move(Transform target);
    }
}