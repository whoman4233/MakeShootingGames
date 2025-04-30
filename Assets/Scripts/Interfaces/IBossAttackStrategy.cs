using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public interface IBossAttackStrategy
    {
        void Attack(Transform shooter);
    }
}
